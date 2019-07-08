using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Ranged : State
{
    private bool projectile_done;

    private RangedEnemy ranged;

    private Vector3 directionAttack;

    private Projectile projectile;

    private float timer;

    private bool destination_done;

    private Vector3 randomPos;

    private bool search_position;



    public override void Enter()
    {

        ranged = GetComponent<RangedEnemy>();

        destination_done = false;

        search_position = true;

        ranged.shoot = false;

    }

    public override void Execute()
    {
        directionAttack = (ranged.enemy_navmesh.transform.position - ranged.player.transform.position).normalized;

        Quaternion lookOnLook = Quaternion.LookRotation(-directionAttack);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookOnLook, 0.5f);

        timer += Time.deltaTime;

        if (search_position)
        {
            randomPos = ranged.player.transform.position + Random.insideUnitSphere * 10;
            if (Vector3.Distance(ranged.player.transform.position, randomPos) > 5)
            {
                search_position = false;
            }
        }

        if (timer < ranged.timeBetweenShots)
        {
            if(!destination_done)
            {
                ranged.enemy_navmesh.SetDestination(randomPos);
                destination_done = true;
            }
        }
        else
        {
            Shoot();
            timer = 0;
            destination_done = false;
            search_position = true;
        }


        if (ranged.distanceToPlayer <= ranged.distanceToFlee) ranged.ChangeState(ranged.flee);
        else if (ranged.distanceToPlayer <= ranged.distanceToChase && ranged.distanceToPlayer > ranged.distanceToAttack) ranged.ChangeState(ranged.chase);
    }



    public override void Exit()
    {


    }

    private void Shoot()
    {
        Vector3 positionProjectile = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z);
        projectile = ranged.gamemanagerScript.GetProjectileNotActive();
        projectile.transform.position = positionProjectile;
        projectile.transform.rotation = transform.rotation;
        projectile_done = true;
    }


}
