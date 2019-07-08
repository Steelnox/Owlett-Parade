using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Trash : State
{
    private TrashEnemy trash;

    //public float frequency = 20.0f;

    //public float magnitude = 0.5f;

    public Vector3 destination;

    private float timer;


    public override void Enter()
    {
        trash = GetComponent<TrashEnemy>();

        destination = new Vector3(trash.player.transform.position.x + Random.Range(-7, 7), trash.player.transform.position.y, trash.player.transform.position.z);
        trash.enemy_navmesh.SetDestination(destination);
        timer = 0;

        trash.myMeshRenderer.material = trash.enemyNormalMat;
    }

    public override void Execute()
    {
        //transform.LookAt(trash.player.transform.position);  Mathf.Sin(Time.time * frequency)* magnitude

        destination = new Vector3(trash.player.transform.position.x + Random.Range(-7, 7), trash.player.transform.position.y, trash.player.transform.position.z);
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            trash.enemy_navmesh.SetDestination(destination);
            timer = 0;

        }

        if (trash.distanceToPlayer <= trash.distanceToFlee) trash.ChangeState(trash.flee);
        else if (trash.distanceToPlayer <= trash.distanceToAttack) trash.ChangeState(trash.prepAttack);
    }

    public override void Exit()
    {
        

    }
}
