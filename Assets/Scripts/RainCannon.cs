using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCannon : MonoBehaviour
{
    public float duration = 10f;
    public int damagePerSecond = 1;

    public List<Enemy> enemiesInside = new List<Enemy>();

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            var enemy = hitColliders[i].GetComponent<Enemy>();

            if (enemy) enemiesInside.Add(enemy);
        }

        Destroy(gameObject, duration);

        StartCoroutine(DoDamage());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null) enemiesInside.Add(other.GetComponent<Enemy>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemiesInside.Contains(enemy)) enemiesInside.Remove(enemy);
        }
    }

    private IEnumerator DoDamage()
    {
        foreach (Enemy enemy in enemiesInside)
        {
            print(damagePerSecond);
            enemy.OnHit(damagePerSecond);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(DoDamage());
    }
}
