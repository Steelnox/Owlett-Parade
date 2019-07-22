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
    //public Rigidbody rigidBody;

    [Header("Basic States")]
    private StateMachine stateMachine = new StateMachine();
    public State currentState;

    [Space]
    public State idle;
    public Dash dash;
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

    [Header("Movement")]

    public CharacterController characterController;

    private Vector3 velocitySmoothing;

    public float smoothSpeedValue = .1f;
    public float walkSpeed;
    public float rotationSpeed = 10f;

    public MovementRestriction MoveRestriction = MovementRestriction.FREE;

    private void Start()
    {
        cooldownManager = CooldownManager.instance;
        healthSystem = GetComponent<IHealth>();
        if (!chamberSuit) ChangeSuit();
        ChangeState(idle);
    }

    private void Update()
    {
        if (MoveRestriction == MovementRestriction.FREE)
        {
            GetInput();
            Move();
        }

        if (currentState == idle)
        {
            if (Input.GetButtonDown("ChamberSuit")) ChangeSuit();
            if (Input.GetButtonDown("QuickDash") && dash.charges.RuntimeValue > 0) ChangeState(dash);
            if (SkillAvailable(stealSuit)) ChangeState(stealSuit);
            if (SkillAvailable(currentSuit.attack)) ChangeState(currentSuit.attack);
            if (SkillAvailable(currentSuit.ability1)) ChangeState(currentSuit.ability1);
            if (SkillAvailable(currentSuit.ability2)) ChangeState(currentSuit.ability2);
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

        walkSpeed = currentSuit.moveSpeed;
    }

    #endregion

    #region StateMethods

    public void ChangeState(State state)
    {
        stateMachine.ChangeState(state);

        currentState = stateMachine.currentState;
    }

    public bool SkillAvailable(Skill skill)
    {
        return (skill != null && Input.GetButtonDown(skill.button) && skill.available);
    }

    public void ReturnToBaseState()
    {
        ChangeState(idle);
    }

    #endregion

    #region Movement

    private void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, 0, z).normalized;

        suitAnimator.SetBool("Moving", !(x == 0 && z == 0));


    }

    public void Move()
    {
        var targetSpeed = movement.normalized * walkSpeed + Physics.gravity;

        characterController.Move(targetSpeed * Time.deltaTime);

        transform.forward = Vector3.RotateTowards(transform.forward, movement, rotationSpeed * Time.deltaTime, 0);
    }

    public bool CheckFloor(bool stairs)
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3(transform.position.x, characterController.bounds.min.y, transform.position.z);

        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (Physics.Raycast(ray, out hit, 0.2f, LayerMask.GetMask("Floor")))
        {
            if (stairs && hit.transform.tag == "Stair") return true;
            else return true;
        }

        return false;
    }

    #endregion

    public IEnumerator Invulnerable()
    {
        invulnerable = true;

        yield return new WaitForSeconds(invulnerabilityTime);

        invulnerable = false;
    }
}