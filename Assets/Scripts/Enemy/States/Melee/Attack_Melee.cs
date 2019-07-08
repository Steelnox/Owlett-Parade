using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Melee : State
{

    private float timerAttack;

    private bool dmg_done;

    private Melee melee;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float timerDash;


    public override void Enter()
    {

        melee = GetComponent<Melee>();

        startPosition = transform.position;

        endPosition = startPosition + (-melee.directionAttack);

        timerAttack = 0;

        timerDash = 0;

        dmg_done = false;


        
    }

    public override void Execute()
    {

        timerDash += Time.deltaTime;

        melee.collider.enabled = false;

        melee.enemy_navmesh.isStopped = false;

        melee.enemy_navmesh.Move(melee.GetDirectionFromTo_N(startPosition, endPosition) * 0.25f);

        melee.enemy_animator.SetBool("Attack", true);


        melee.enemy_animator.enabled = true;

        Collider[] entities = Physics.OverlapSphere(transform.position, 2);

        foreach (Collider col in entities)
        {

            if (col.gameObject.tag == "Player" && !dmg_done)
            {
                AddDamage();
            }
        }

        if (timerDash >= 0.25f)
        {
            FinishAttack();
        }

    }



    public override void Exit()
    {
        timerAttack = 0;
        timerDash = 0;
        dmg_done = false;

        melee.enemy_animator.SetBool("Attack", false);

        melee.collider.enabled = true;

    }

    #region Functions



    private void AddDamage()
    {
        Debug.Log("dmgdone");
        //melee.player.healthSystem.GetDamaged(melee.damage);
        dmg_done = true;
    }

    private void FinishAttack()
    {
        if (melee.GetDistance(melee.player.transform.position) <= melee.distanceToChase) melee.ChangeState(melee.chase);
        else if (melee.GetDistance(melee.player.transform.position) > melee.distanceToChase) melee.ChangeState(melee.patrol);
    }
    #endregion
}
