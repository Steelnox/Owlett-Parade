using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPassive : MonoBehaviour
{
    public static LightPassive instance;

    public List<Enemy> markedEnemies = new List<Enemy>();
    public List<Enemy> oldMarkedEnemies = new List<Enemy>();
    public List<Enemy> jumpedEnemies = new List<Enemy>();

    public GameObject markPrefab;

    public List<LightMark> marks = new List<LightMark>();

    public float markedAgainCooldown = 3f;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
        /*
        //For Testing Purposes
        var temp = FindObjectsOfType<Melee>();

        for (int i = 0; i < temp.Length; i++)
        {
            markedEnemies.Add(temp[i]);
        }

        */
    }

    public void MarkEnemy(Enemy enemy)
    {
        if (oldMarkedEnemies.Contains(enemy)) return;

        markedEnemies.Add(enemy);

        GameObject mark = Instantiate(markPrefab, enemy.transform.position, Quaternion.identity);

        LightMark lightMark = mark.GetComponent<LightMark>();

        lightMark.currentEnemy = enemy;
        marks.Add(lightMark);
    }

    public bool BreakMark(Enemy enemy)
    {
        if (markedEnemies.Contains(enemy))
        {
            markedEnemies.Remove(enemy);

            foreach (LightMark mark in marks)
            {
                if (mark.currentEnemy == enemy)
                {
                    marks.Remove(mark);
                    Destroy(mark.gameObject);

                    break;
                }
            }

            StartCoroutine(CanBeMarkedAgain(enemy));

            return true;
        }

        return false;
    }

    public IEnumerator CanBeMarkedAgain(Enemy enemy)
    {
        if (enemy) oldMarkedEnemies.Add(enemy);

        yield return new WaitForSeconds(markedAgainCooldown);

        oldMarkedEnemies.Remove(enemy);
    }
}