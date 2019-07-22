using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDash : Skill
{
    public float dashSpeed = 12f;
    public float dashDuration = 0.4f;

    public bool mouseDirection = false;

    public float timer = 0;
    public float initalSpeed = 0;

    public bool brokeMark = false;

    public CapsuleCollider colliderTrigger;

    public override void Enter()
    {
        controller.MoveRestriction = MovementRestriction.DASHING;

        controller.suitAnimator.SetBool("Moving", false);
        controller.suitAnimator.SetBool("Dashing", true);

        colliderTrigger.isTrigger = true;

        if (mouseDirection)
            controller.transform.forward = MathExtension.ForwardWithoutY(transform, MathExtension.MouseWorldPosition("Floor"));
    }

    public override void Execute()
    {
        controller.characterController.Move(controller.transform.forward * dashSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= dashDuration)
        {
            timer = 0;

            colliderTrigger.isTrigger = false;
            controller.ReturnToBaseState();
        }
    }

    public override void Exit()
    {
        if (!brokeMark) base.Exit();

        controller.MoveRestriction = MovementRestriction.FREE;

        brokeMark = false;
        controller.suitAnimator.SetBool("Dashing", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (controller.currentState == this && other.CompareTag("Monster") && !brokeMark)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            brokeMark = LightPassive.instance.BreakMark(enemy);

            enemy.OnHit(brokeMark ? damage * 2 : damage);
        }
    }
}
