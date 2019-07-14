using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///   BASE CLASS FOR THE ENEMIES, USE INHERITANCE TO CREATE OTHER CLASSES OF ENEMIES (for example: RangedEnemy, MeleeEnemy, ElitEnemy)
public abstract class Enemy : MonoBehaviour
{
    //Properties that ALL enemies will have, add if needed
    protected StateMachine stateMachine = new StateMachine();
    public State currentState;

    public Animator enemy_animator;

    public Controller player;

    public NavMeshAgent enemy_navmesh;

    public GameManager gamemanagerScript;


    public enum EnemyType { MELEE, RANGED } //If other types needed, just add
    public EnemyType enemyType;


    public float distanceToPlayer;

    public Material staggerMaterial;

    public State staggerState;


    public enum SuitType { NONE, LIGHT, HEAVY, CC }
    public SuitType suitType;

    #region Stats

    public int maxHealth;
    public int currentHealth;
    public int damage;
    public float speed;
    public int enemyScore;

    public float distanceToChase;

    [Tooltip("Distance to start preparing the attack")]
    public float distanceToAttack;

    [Tooltip("Enemy can stagger the player")]
    public bool CanStagger;

    [Tooltip("Enemy can be staggered")]
    public bool stagger;


    #endregion

    private void Start()
    {
        player = Controller.instance;

        gamemanagerScript = GameManager.instance;

        enemy_navmesh.speed = speed;
    }


    //Functions that ALL enemies will have, add if needed (override later with it's own implementation)
    public virtual void OnHit(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (currentHealth == 0) this.gameObject.SetActive(false);
        if (stagger) ChangeState(staggerState);

        gamemanagerScript.ResetMultiplier(enemyScore);

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

    public bool RandomBool()
    {
        return (Random.value > 0.5f);
    }

    public Vector3 GetDirectionFromTo_N(Vector3 from, Vector3 to)
    {
        Vector3 result;

        return result = (to - from).normalized;
    }

}
