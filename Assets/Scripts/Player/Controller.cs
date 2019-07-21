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
    public Suit noSuit;

    public List<Suit> allSuits;

    public SuitValue currentSuitValue;
    public SuitValue chamberSuitValue;

    public Animator suitAnimator;
    public IHealth healthSystem;

    public float invulnerabilityTime = 1;
    public bool invulnerable = false;

    private void Start()
    {
        cooldownManager = CooldownManager.instance;
        healthSystem = GetComponent<IHealth>();
        if (!chamberSuit) ChangeSuit();
        ChangeState(idle);
    }

    private void Update()
    {
        if (currentState == idle || currentState == walk)
        {
            if (Input.GetButtonDown("ChamberSuit")) ChangeSuit();

            if (Input.GetButtonDown(stealSuit.button) && stealSuit.available) ChangeState(stealSuit);

            if (currentSuit.attack && Input.GetButtonDown(currentSuit.attack.button) && currentSuit.attack.available)
                ChangeState(currentSuit.attack);

            if (currentSuit.ability1 && Input.GetButtonDown(currentSuit.ability1.button) && currentSuit.ability1.available)
                ChangeState(currentSuit.ability1);

            if (currentSuit.ability2 && Input.GetButtonDown(currentSuit.ability2.button) && currentSuit.ability2.available)
                ChangeState(currentSuit.ability2);
        }

        stateMachine.ExecuteState();
    }

    #region SuitMethods

    public void ChangeSuit()
    {
        if (currentSuit == noSuit) return;

        chamberSuit = chamberSuit == noSuit ? null : chamberSuit;

        if (chamberSuit)
        {
            currentSuit.gameObject.SetActive(false);

            Suit tempSuit = currentSuit;

            currentSuit = chamberSuit;
            currentSuitValue.RuntimeValue = currentSuit;

            currentSuit.EquipSuit();
            chamberSuit = tempSuit;
            chamberSuitValue.RuntimeValue = chamberSuit;

            ReturnToBaseState();
        }
        else
        {
            chamberSuitValue.RuntimeValue = noSuit;
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