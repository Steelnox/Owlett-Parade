using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///   BASE CLASS FOR THE ENEMIES, USE INHERITANCE TO CREATE OTHER CLASSES OF ENEMIES (for example: RangedEnemy, MeleeEnemy, ElitEnemy)
public abstract class Enemy : MonoBehaviour
{
    //Properties that ALL enemis will have, add if needed
    protected StateMachine stateMachine = new StateMachine();
    public State currentState;

    public Animator enemy_animator;

    public Controller player;

    public NavMeshAgent enemy_navmesh;


    public enum EnemyType { MELEE, RANGED } //If other types needed, just add
    public EnemyType enemyType;

    #region Stats

    public int maxHealth;
    public int currentHealth;
    public int damage;
    public float speed;

    public float distanceToChase;
    public float distanceToAttack;

    #endregion

    private void Start()
    {
        player = Controller.instance;

    }

    //Functions that ALL enemies will have, add if needed (override later with it's own implementation)
    public virtual void OnHit(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
    }

    public void ChangeState(State state)
    {
        stateMachine.ChangeState(state);

        currentState = stateMachine.currentState;
    }


    public float GetDistance(Vector3 targetpos)
    {
        return Vector3.Distance(targetpos, transform.position);
    }

}
