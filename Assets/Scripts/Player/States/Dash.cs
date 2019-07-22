using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dash : State
{
    private Controller controller;

    public float dashSpeed = 7f;
    public float dashDuration = 0.25f;

    public bool mouseDirection = false;

    public IntValue charges;
    public IntValue Maxcharges;

    public FloatValue dashReloadTime;

    public string button = "QuickDash";

    private bool reloading;

    public bool Available => charges.RuntimeValue > 1 && Input.GetButtonDown(button);

    public override void Enter()
    {
        charges.RuntimeValue -= 1;

        Reload();

        controller = Controller.instance;

        controller.MoveRestriction = MovementRestriction.DASHING;
        controller.suitAnimator.SetBool("Moving", false);
        controller.suitAnimator.SetBool("Dashing", true);
        controller.invulnerable = true;

        Vector3 direction = (MathExtension.MouseWorldPosition("Floor") - controller.transform.position).normalized;

        if (mouseDirection) controller.transform.forward = direction;

        StartCoroutine(DoDash());
    }

    public override void Exit()
    {
        controller.MoveRestriction = MovementRestriction.FREE;
        controller.suitAnimator.SetBool("Dashing", false);
        controller.invulnerable = false;
    }

    public IEnumerator DoDash()
    {
        float timer = 0f;

        while (timer < dashDuration)
        {
            controller.characterController.Move(controller.transform.forward * dashSpeed * Time.deltaTime);

            timer += Time.deltaTime;

            yield return null;
        }

        controller.ReturnToBaseState();
    }

    public void Reload()
    {
        if (reloading) return;

        reloading = true;

        CooldownManager.instance.SetCooldownAction(dashReloadTime.RuntimeValue, GainCharge);
    }

    public void GainCharge()
    {
        if (charges.RuntimeValue < Maxcharges.RuntimeValue) charges.RuntimeValue += 1;

        reloading = false;

        if (charges.RuntimeValue < Maxcharges.RuntimeValue) Reload();
    }
}
