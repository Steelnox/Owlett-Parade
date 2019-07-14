using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepAttack_Melee : State
{
    private Melee melee;

    private float timer;

    [Tooltip("Time need wait enemy for attack")]
    public float timeWaitAttack;


    public override void Enter()
    {

        melee = GetComponent<Melee>();

        melee.enemy_navmesh.isStopped = true;
        timer = 0;

        melee.directionAttack = (melee.enemy_navmesh.transform.position - melee.player.transform.position).normalized;

        melee.enemy_animator.enabled = false;


    }

    public override void Execute()
    {
        Quaternion lookOnLook = Quaternion.LookRotation(-melee.directionAttack);

        transform.rotation = Quaternion.Slerp(melee.enemy_navmesh.transform.rotation, lookOnLook, Time.deltaTime * 1.5f);


       timer += Time.deltaTime;

        if (timer > timeWaitAttack) melee.ChangeState(melee.attack);
       

    }

    public override void Exit()
    {
        if(melee.currentHealth > 0) melee.enemy_navmesh.isStopped = false;


    }
}
