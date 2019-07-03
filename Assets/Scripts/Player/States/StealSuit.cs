using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealSuit : Skill
{
    public override void Enter()
    {
        controller.transform.LookAt(MathExtension.MouseWorldPosition("Floor"));
        controller.suitAnimator.SetTrigger("Attack");
    }

    public override void Execute()
    {
        controller.rigidBody.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
