using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagger_Trash : State
{

    float timer;

    public float timeStaggered;

    private TrashEnemy trash;

    private Vector3 directionKnockback;

    

    public override void Enter()
    {

        trash = GetComponent<TrashEnemy>();

        trash.myMeshRenderer.material = trash.staggerMaterial;
        if(trash.currentHealth > 0) trash.enemy_navmesh.isStopped = true;
        timer = 0;
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= timeStaggered)
        {
           
            if (trash.currentHealth > 0)
            {
                if (trash.distanceToPlayer <= trash.distanceToFlee) trash.ChangeState(trash.flee);
                else if (trash.distanceToPlayer <= trash.distanceToChase) trash.ChangeState(trash.chase);
                else if (trash.distanceToPlayer > trash.distanceToChase) trash.ChangeState(trash.patrol);
            }
        }
    }

    public override void Exit()
    {
        if(trash.currentHealth > 0)
        {
            trash.enemy_navmesh.isStopped = false;
            trash.enemy_navmesh.ResetPath();
        }
        
        trash.myMeshRenderer.material = trash.enemyNormalMat;
    }
}
