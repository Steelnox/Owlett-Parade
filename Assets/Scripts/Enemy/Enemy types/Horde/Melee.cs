using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee : Enemy
{
    public State patrol;
    public State chase;
    public State attack;

    public CapsuleCollider collider;


    void Start()
    {
        player = Controller.instance;
        enemyType = EnemyType.MELEE;

        enemy_navmesh = GetComponent<NavMeshAgent>();

        collider = GetComponent<CapsuleCollider>();

        ChangeState(patrol);
    }

    void Update()
    {
      
        stateMachine.ExecuteState();
        //Debug.Log(currentState);

    }



}
