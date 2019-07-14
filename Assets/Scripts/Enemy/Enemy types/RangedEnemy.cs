using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : Enemy
{
    public State chase;
    public State attack;
    public State flee;
    public State patrol;

    public bool shoot;

    public CapsuleCollider C_collider;

    public Projectile projectile;

    [Tooltip("Distance to start running away from the player")]
    public float distanceToFlee;

    [Tooltip("Time between one shoot and the other one, this time the enemy move around player randomly")]
    public float timeBetweenShots;

    public Renderer myMeshRenderer;
    public Material normalMaterial;

    void Start()
    {
        player = Controller.instance;

        gamemanagerScript = GameManager.instance;


        enemyType = EnemyType.RANGED;

        enemy_navmesh = GetComponent<NavMeshAgent>();

        C_collider = GetComponent<CapsuleCollider>();

        currentHealth = maxHealth;

        shoot = false;

        ChangeState(chase);

    }

    void Update()
    {
        distanceToPlayer = GetDistance(player.transform.position);

        stateMachine.ExecuteState();

    }
}
