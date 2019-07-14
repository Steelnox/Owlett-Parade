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
    public State flee;
    public State patrol;

    [Tooltip("Distance to start running away from the player")]
    public float distanceToFlee;


    public Renderer myMeshRenderer;

    public Material enemyNormalMat;
    public Material enemyAngryMat;


    #endregion

    void Start()
    {
        player = Controller.instance;
        enemyType = EnemyType.MELEE;

        enemy_navmesh = GetComponent<NavMeshAgent>();

        gamemanagerScript = GameManager.instance;

        currentHealth = maxHealth;

        ChangeState(chase);
    }

    void Update()
    {
        stateMachine.ExecuteState();

        distanceToPlayer = GetDistance(player.transform.position);

        //Debug.Log(currentState);
    }

}
