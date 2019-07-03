using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : Skill
{
    public GameObject effects;
    public Transform spawnPosition;

    public override void Enter()
    {
        controller.transform.LookAt(MathExtension.MouseWorldPosition("Floor"));
        controller.suitAnimator.SetTrigger("Attack");
    }

    public override void Execute()
    {
        controller.rigidBody.velocity = Vector3.zero;
    }

    public void OnAttack()
    {
        if (controller.currentState != this) return;

        GameObject attack = Instantiate(effects, spawnPosition.position, transform.rotation);            
    }       
}
