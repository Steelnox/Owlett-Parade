using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAbilityOne : MonoBehaviour
{
    [SerializeField] private GameObject particles;

    [SerializeField] private LayerMask mask;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float destroyTime = 0.4f;
    [SerializeField] private float jumpRadius = 2.5f;

    [SerializeField] private int damage = 1;

    private Enemy firstEnemy;

    private LightPassive lightPassive;

    #region Jumped Attack

    public bool redirected = false;
    public Enemy target;

    public float closeEnoughRadius = 0.1f;

    #endregion

    private void Start()
    {
        lightPassive = LightPassive.instance;

        if (redirected)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.forward = (targetPos - transform.position).normalized;
        }

        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        if (!redirected) transform.position += transform.forward * speed * Time.deltaTime;

        else
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (Distance(gameObject, target.gameObject) < closeEnoughRadius)
            {
                InstantiateEffects();
                target.OnHit(damage);
                JumpToOtherEnemies(target);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster") && !redirected)
        {
            InstantiateEffects();

            firstEnemy = other.GetComponent<Enemy>();
            firstEnemy.OnHit(damage);

            JumpToOtherEnemies(firstEnemy);
        }
    }

    private void JumpToOtherEnemies(Enemy origin)
    {
        if (lightPassive.BreakMark(origin))
        {
            lightPassive.jumpedEnemies.Add(origin);

            DetectEnemiesOnRadius(origin);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DetectEnemiesOnRadius(Enemy origin)
    {
        Collider[] entities = Physics.OverlapSphere(origin.transform.position, jumpRadius, mask);

        List<Enemy> enemiesInRadius = new List<Enemy>();

        for (int i = 0; i < entities.Length; i++)
        {
            if (entities[i].CompareTag("Monster"))
            {
                if (!lightPassive.jumpedEnemies.Contains(entities[i].GetComponent<Enemy>()))
                    enemiesInRadius.Add(entities[i].GetComponent<Enemy>());
            }
        }

        if (enemiesInRadius.Count > 0)
        {
            Enemy closestEnemy = enemiesInRadius[0];

            foreach (Enemy enemy in enemiesInRadius)
            {
                if (Distance(enemy.gameObject, origin.gameObject) < Distance(closestEnemy.gameObject, origin.gameObject))
                {
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                enemiesInRadius.Remove(closestEnemy);

                lightPassive.jumpedEnemies.Add(closestEnemy);

                LightAbilityOne shot = Instantiate(this, transform.position, Quaternion.identity);
                shot.speed = redirected ? speed : speed / 2;
                shot.redirected = true;
                shot.target = closestEnemy;
            }

            Enemy secondClosestEnemy = enemiesInRadius[0];

            foreach (Enemy enemy in enemiesInRadius)
            {
                if (Distance(enemy.gameObject, origin.gameObject) < Distance(secondClosestEnemy.gameObject, origin.gameObject))
                {
                    secondClosestEnemy = enemy;
                }
            }

            if (secondClosestEnemy != null)
            {
                lightPassive.jumpedEnemies.Add(secondClosestEnemy);

                LightAbilityOne shot = Instantiate(this, transform.position, Quaternion.identity);
                shot.speed = redirected ? speed : speed / 2;
                shot.redirected = true;
                shot.target = secondClosestEnemy;
            }
        }

        Destroy(gameObject);
    }

    private void InstantiateEffects()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
        CooldownManager.instance.StopTime();
        CooldownManager.instance.SetTime();
    }

    private float Distance(GameObject a, GameObject b)
    {
        Vector3 aPos = a.transform.position;
        Vector3 bPos = b.transform.position;

        return Vector2.Distance(new Vector2(aPos.x, aPos.z), new Vector2(bPos.x, bPos.z));
    }
}
