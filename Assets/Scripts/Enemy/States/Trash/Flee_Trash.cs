using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee_Trash : State
{
    private TrashEnemy trash;

    //public float frequency = 20.0f;

    //public float magnitude = 0.5f;

    public Vector3 destination;

    private float timer;

    public override void Enter()
    {
        trash = GetComponent<TrashEnemy>();

        trash.myMeshRenderer.material = trash.enemyNormalMat;

    }

    public override void Execute()
    {

        Vector3 dirToPlayer = transform.position - trash.player.transform.position;

        Vector3 newpos = transform.position + dirToPlayer;

        trash.enemy_navmesh.SetDestination(newpos);

        if (trash.distanceToPlayer <= trash.distanceToAttack && trash.distanceToPlayer > trash.distanceToFlee) trash.ChangeState(trash.prepAttack);
        else if (trash.distanceToPlayer <= trash.distanceToChase && trash.distanceToPlayer > trash.distanceToAttack) trash.ChangeState(trash.chase);


    }

    public override void Exit()
    {


    }
}
