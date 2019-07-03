using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}

public abstract class Skill : State
{
    protected Controller controller;

    public string button;
    public bool available = true;

    public int cost;

    public int damage = 1;
    public int multiplier = 1;

    public bool hasCooldown = true;
    public float cooldown = 0f;

    public void Start()
    {
        controller = Controller.instance;
    }

    public override void Exit()
    {
        if (hasCooldown)
        {
            available = false;
            controller.cooldownManager.SetCooldown(this);
        }

        controller.currentSuit.suitHealthSystem.GetDamaged(cost);
    }
}

