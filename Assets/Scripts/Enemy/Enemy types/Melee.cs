using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Melee : Enemy
{
    public State patrol;
    public State chase;
    public State attack;
    public State prepAttack;

    public CapsuleCollider collider;

    public Vector3 directionAttack;

    public Material normalMaterial;
    public Renderer myMeshRenderer;



    void Start()
    {
        player = Controller.instance;
        enemyType = EnemyType.MELEE;

        enemy_navmesh = GetComponent<NavMeshAgent>();

        collider = GetComponent<CapsuleCollider>();

        currentHealth = maxHealth;

        gamemanagerScript = GameManager.instance;

        ChangeState(patrol);
    }

    void Update()
    {
      
        stateMachine.ExecuteState();
        //Debug.Log(currentState);

    }



}
