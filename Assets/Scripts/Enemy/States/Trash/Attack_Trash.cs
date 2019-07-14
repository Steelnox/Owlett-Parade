using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Trash : State
{
    private TrashEnemy trash;

    public Vector3 destination;

    private Vector3 directionAttack;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private float timer;

    public bool dmg_done;

    public override void Enter()
    {
        trash = GetComponent<TrashEnemy>();

        //trash.enemy_navmesh.speed *= 2;

        trash.enemy_navmesh.ResetPath();

        directionAttack = (trash.enemy_navmesh.transform.position - trash.player.transform.position).normalized;


        startPosition = transform.position;

        endPosition = startPosition + (-directionAttack);

        timer = 0;
        //destination = new Vector3(trash.player.transform.position.x, trash.player.transform.position.y, trash.player.transform.position.z);
        //trash.enemy_navmesh.SetDestination(destination);

        dmg_done = false;
    }

    public override void Execute()
    {
        timer += Time.deltaTime;

        if (timer < 1.2f) trash.enemy_navmesh.Move(trash.GetDirectionFromTo_N(startPosition, endPosition) * 0.1f);

        else Invoke("ChangeAttackState", 2);

        Collider[] entities = Physics.OverlapSphere(transform.position, 1);

        foreach (Collider col in entities)
        {

            if (col.gameObject.tag == "Player" && !dmg_done)
            {
                AddDamage();
            }
        }

    }

    public override void Exit()
    {

    }


    private void ChangeAttackState()
    {
        if (trash.distanceToPlayer <= trash.distanceToFlee) trash.ChangeState(trash.flee);
        else if (trash.distanceToPlayer <= trash.distanceToChase) trash.ChangeState(trash.chase);
        else if (trash.distanceToPlayer > trash.distanceToChase) trash.ChangeState(trash.patrol);


    }

    public void AddDamage()
    {
        Debug.Log("dmgdone");
       
        trash.player.healthSystem.GetDamaged(trash.damage);
        dmg_done = true;
    }
}
