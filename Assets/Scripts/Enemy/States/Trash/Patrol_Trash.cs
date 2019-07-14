using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Trash : State
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

    private TrashEnemy trash;

    public override void Enter()
    {
        trash = GetComponent<TrashEnemy>();
        distanceToPoint = 1.0f;

        AssignRandom();
    }

    public override void Execute()
    {
        if (trash.GetDistance(patrolPoint) < distanceToPoint) AssignRandom();

        if (trash.GetDistance(trash.player.transform.position) <= trash.distanceToChase) trash.ChangeState(trash.chase);


    }

    public void AssignRandom()
    {
        randomPosX = Random.Range(PosLeft.transform.position.x, PosRight.transform.position.x);
        randomPosZ = Random.Range(PosLeft.transform.position.z, PosRight.transform.position.z);

        patrolPoint = new Vector3(randomPosX, transform.position.y, randomPosZ);

        trash.enemy_navmesh.SetDestination(patrolPoint);
    }



    public float GetDistance(Vector3 patrolPoint)
    {
        return Vector3.Distance(patrolPoint, transform.position);
    }
}
