using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagger_Melee : State
{
    float timer;

    public float timeStaggered;

    private Melee melee;

    private Vector3 directionKnockback;

    public override void Enter()
    {

        melee = GetComponent<Melee>();

        melee.myMeshRenderer.material = melee.staggerMaterial;

        melee.enemy_animator.enabled = false;

        if (melee.currentHealth > 0) melee.enemy_navmesh.isStopped = true;
        timer = 0;
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (timer >= timeStaggered)
        {

            if (melee.currentHealth > 0)
            {
                if (melee.distanceToPlayer <= melee.distanceToAttack) melee.ChangeState(melee.prepAttack);

                else if (melee.distanceToPlayer <= melee.distanceToChase) melee.ChangeState(melee.chase);

            }
        }
    }

    public override void Exit()
    {
        if (melee.currentHealth > 0)
        {
            melee.enemy_navmesh.isStopped = false;
            melee.enemy_navmesh.ResetPath();
        }

        melee.enemy_animator.enabled = true;

        melee.myMeshRenderer.material = melee.normalMaterial;
    }
}
