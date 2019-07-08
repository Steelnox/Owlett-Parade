using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee_Ranged : State
{
    private RangedEnemy ranged;

    public override void Enter()
    {
        ranged = GetComponent<RangedEnemy>();

        ranged.enemy_navmesh.isStopped = false;


    }

    public override void Execute()
    {
        Vector3 dirToPlayer = transform.position - ranged.player.transform.position;

        Vector3 newpos = transform.position + dirToPlayer;

        ranged.enemy_navmesh.SetDestination(newpos);

        if (ranged.distanceToPlayer <= ranged.distanceToChase && ranged.distanceToPlayer > ranged.distanceToAttack) ranged.ChangeState(ranged.chase);
        else if (ranged.distanceToPlayer <= ranged.distanceToAttack && ranged.distanceToPlayer > ranged.distanceToFlee) ranged.ChangeState(ranged.attack);


    }

    public override void Exit()
    {

    }
}
