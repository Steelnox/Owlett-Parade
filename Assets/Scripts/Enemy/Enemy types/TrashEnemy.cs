using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TrashEnemy : Enemy
{

    #region States

    public State chase;
    public State prepAttack;
    public State attack;

    public Material enemyMat;


    #endregion

    void Start()
    {
        player = Controller.instance;
        enemyType = EnemyType.MELEE;

        enemy_navmesh = GetComponent<NavMeshAgent>();

        //enemyMat = GetComponent<Material>();

        ChangeState(chase);
    }

    void Update()
    {
        stateMachine.ExecuteState();
        //Debug.Log(currentState);
    }
}
