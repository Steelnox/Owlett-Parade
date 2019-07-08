using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepAttack_Trash : State
{
    private TrashEnemy trash;

    private float timer;

    [Tooltip("Time need wait enemy for attack")]

    public float timeWaitAttack;

    private Vector3 directionAttack;
    
    public override void Enter()
    {

        trash = GetComponent<TrashEnemy>();

        trash.enemy_navmesh.isStopped = true;
        timer = 0;

        directionAttack = (trash.enemy_navmesh.transform.position - trash.player.transform.position).normalized;

    }

    public override void Execute()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(-directionAttack);

        transform.rotation = Quaternion.Slerp(trash.enemy_navmesh.transform.rotation, lookOnLook, Time.deltaTime * 1.5f);


        if (trash.player.movement == Vector3.zero)
        {
            timer += Time.deltaTime;

            if (timer >= 0.5f) trash.myMeshRenderer.material = trash.enemyAngryMat;

            if (timer > timeWaitAttack) trash.ChangeState(trash.attack);
        }
       else
        {

            if (trash.distanceToPlayer <= trash.distanceToFlee) trash.ChangeState(trash.flee);
            else if (trash.distanceToPlayer <= trash.distanceToChase) trash.ChangeState(trash.chase);
        }

    }

    public override void Exit()
    {
        trash.enemy_navmesh.isStopped = false;


    }
}
