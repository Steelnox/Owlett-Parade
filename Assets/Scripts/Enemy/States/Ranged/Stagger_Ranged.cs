using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagger_Ranged : State
{
    float timer;

    public float timeStaggered;

    private RangedEnemy ranged;

    private Vector3 directionKnockback;



    public override void Enter()
    {

        ranged = GetComponent<RangedEnemy>();

        ranged.myMeshRenderer.material = ranged.staggerMaterial;
        if (ranged.currentHealth > 0) ranged.enemy_navmesh.isStopped = true;
        timer = 0;
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= timeStaggered)
        {

            if (ranged.currentHealth > 0)
            {
                if (ranged.distanceToPlayer <= ranged.distanceToFlee) ranged.ChangeState(ranged.flee);
                else if (ranged.distanceToPlayer <= ranged.distanceToChase) ranged.ChangeState(ranged.chase);
                else if (ranged.distanceToPlayer > ranged.distanceToChase) ranged.ChangeState(ranged.patrol);

            }
        }
    }

    public override void Exit()
    {
        if (ranged.currentHealth > 0)
        {
            ranged.enemy_navmesh.isStopped = false;
            ranged.enemy_navmesh.ResetPath();
        }

        ranged.myMeshRenderer.material = ranged.normalMaterial;
    }
}
