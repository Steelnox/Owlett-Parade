using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Ranged : State
{
    #region Variables
    float randomPosX;
    float randomPosZ;

    Vector3 patrolPoint;
    float distanceToPoint;
    [Space(10)]

    public GameObject PosRight;
    public GameObject PosLeft;
    #endregion

    private RangedEnemy ranged;

    public override void Enter()
    {
        ranged = GetComponent<RangedEnemy>();
        distanceToPoint = 1.0f;

        AssignRandom();
    }

    public override void Execute()
    {
        if (ranged.GetDistance(patrolPoint) < distanceToPoint) AssignRandom();

        if (ranged.GetDistance(ranged.player.transform.position) <= ranged.distanceToChase) ranged.ChangeState(ranged.chase);


    }

    public void AssignRandom()
    {
        randomPosX = Random.Range(PosLeft.transform.position.x, PosRight.transform.position.x);
        randomPosZ = Random.Range(PosLeft.transform.position.z, PosRight.transform.position.z);

        patrolPoint = new Vector3(randomPosX, transform.position.y, randomPosZ);

        ranged.enemy_navmesh.SetDestination(patrolPoint);
    }



    public float GetDistance(Vector3 patrolPoint)
    {
        return Vector3.Distance(patrolPoint, transform.position);
    }
}
