using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller : MonoBehaviour
{
    #region Singleton

    public static Controller instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
    }

    #endregion

    public CooldownManager cooldownManager;
    public Vector3 movement;
    public Rigidbody rigidBody;

    [Header("Basic States")]
    private StateMachine stateMachine = new StateMachine();
    public State currentState;

    [Space]
    public State idle;
    public State walk;
    public State dash;
    public Skill stealSuit;

    [Header("Suit")]

    public Suit currentSuit;
    public Suit chamberSuit;

    public Animator suitAnimator;
    public IHealth healthSystem;

    public float invulnerabilityTime = 1;
    public bool invulnerable = false;

    private void Start()
    {
        healthSystem = GetComponent<IHealth>();

        cooldownManager = CooldownManager.instance;

        ChangeState(idle);
    }

    private void Update()
    {
        if (currentState == idle || currentState == walk)
        {
            if (Input.GetKeyDown(KeyCode.Q)) ChangeSuit();
            //else if (Input.GetButtonDown(stealSuit.button) && stealSuit.available) ChangeState(stealSuit);
            if (currentSuit == null) return;
            if (Input.GetButtonDown(currentSuit.attack.button) && currentSuit.attack.available) ChangeState(currentSuit.attack);
            if (Input.GetButtonDown(currentSuit.ability1.button) && currentSuit.ability1.available) ChangeState(currentSuit.ability1);
            if (Input.GetButtonDown(currentSuit.ability2.button) && currentSuit.ability2.available) ChangeState(currentSuit.ability2);
        }

        stateMachine.ExecuteState();
    }

    #region SuitMethods

    private void ChangeSuit()
    {
        if (chamberSuit)
        {
            currentSuit.gameObject.SetActive(false);

            Suit tempSuit = currentSuit;

            currentSuit = chamberSuit;
            currentSuit.EquipSuit();
            chamberSuit = tempSuit;
        }
    }

    #endregion

    #region StateMethods

    public void ChangeState(State state)
    {
        stateMachine.ChangeState(state);

        currentState = stateMachine.currentState;
    }

    public void ReturnToBaseState()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            ChangeState(walk);
        }
        else
        {
            ChangeState(idle);
        }
    }

    #endregion

    public IEnumerator Invulnerable()
    {
        invulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        invulnerable = false;
    }
}