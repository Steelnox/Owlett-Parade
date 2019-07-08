using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Ranged : State
{
    private RangedEnemy ranged;
    private float timer;

    public override void Enter()
    {
        ranged = GetComponent<RangedEnemy>();

        timer = 0;

        ranged.enemy_navmesh.ResetPath();

        ranged.enemy_navmesh.isStopped = false;
        
    }

    public override void Execute()
    {

        if (ranged.distanceToPlayer > ranged.distanceToAttack)
        {

            ranged.enemy_navmesh.SetDestination(ranged.player.transform.position);
        }

        timer += Time.deltaTime;
        if (ranged.distanceToPlayer <= ranged.distanceToFlee) ranged.ChangeState(ranged.flee);
        else if (ranged.distanceToPlayer <= ranged.distanceToAttack && ranged.distanceToPlayer > ranged.distanceToFlee) ranged.ChangeState(ranged.attack);
        //else if (distanceToPlayer >= ranged.distanceToChase) ranged.ChangeState(ranged.patrol);
        


    }

    public override void Exit()
    {

    }
}
