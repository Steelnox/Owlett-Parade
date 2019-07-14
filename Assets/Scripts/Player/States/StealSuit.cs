using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealSuit : Skill
{
    public float range;
    public float radius;

    public override void Enter()
    {
        controller.transform.LookAt(MathExtension.MouseWorldPosition("Floor"));
        controller.suitAnimator.SetTrigger("Attack");
        Attack();

        controller.ReturnToBaseState();
    }

    public override void Execute()
    {
        controller.rigidBody.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void Attack()
    {
        var end = transform.position + (transform.forward * range);
        var hits = Physics.OverlapCapsule(transform.position, end, radius);

        List<Enemy> eliteEnemies = new List<Enemy>();

        for (int i = 0; i < hits.Length; i++)
        {
            Enemy enemy = hits[i].GetComponent<Enemy>();

            if (enemy)
            {
                enemy.OnHit(damage);

                if (enemy.currentHealth != 0 || enemy.suitType == SuitType.NONE) continue;

                eliteEnemies.Add(enemy);
            }
        }

        if (eliteEnemies.Count != 0) ProcessSuit(GetClosestEnemy(eliteEnemies));
    }

    private Enemy GetClosestEnemy(List<Enemy> enemies)
    {
        Enemy closestEnemy = enemies[0];

        foreach (Enemy enemy in enemies)
        {
            if ((closestEnemy.transform.position - transform.position).magnitude > (enemy.transform.position - transform.position).magnitude)
            {
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void ProcessSuit(Enemy enemy)
    {
        if (controller.currentSuit.suitType == SuitType.NONE) ChooseSuit(enemy).EquipSuit();
        else if (controller.currentSuit.suitType == enemy.suitType) controller.currentSuit.HealSuit(true, 0);
        else if (controller.chamberSuit && controller.chamberSuit.suitType == enemy.suitType) controller.chamberSuit.HealSuit(true, 0);
        else { controller.chamberSuit = ChooseSuit(enemy); controller.ChangeSuit(); }
    }

    private Suit ChooseSuit(Enemy enemy)
    {
        foreach (Suit suit in controller.allSuits)
        {
            if (enemy.suitType == suit.suitType) return suit;
        }

        return controller.noSuit;
    }
}
