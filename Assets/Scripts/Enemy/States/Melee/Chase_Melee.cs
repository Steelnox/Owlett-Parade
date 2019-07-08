using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Melee : State
{
    private Melee melee;

    private float timer;

    public override void Enter()
    {
        melee = GetComponent<Melee>();
        melee.enemy_animator.SetBool("Run", true);
        melee.enemy_navmesh.speed = melee.speed * 2;

        timer = 0;

    }

    public override void Execute()
    {

        melee.enemy_navmesh.SetDestination(melee.player.transform.position);

        timer += Time.deltaTime;

        if(timer >= 1f)
        {
            if (melee.GetDistance(melee.player.transform.position) >= melee.distanceToChase) melee.ChangeState(melee.patrol);
            if (melee.GetDistance(melee.player.transform.position) <= melee.distanceToAttack) melee.ChangeState(melee.prepAttack);
        }

        
    }

    public override void Exit()
    {
        melee.enemy_navmesh.speed = melee.speed * 0.5f;

        melee.enemy_animator.SetBool("Run", false);

    }

}
