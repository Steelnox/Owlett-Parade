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

    private bool reloading;

    public override void Enter()
    {
        if (charges.RuntimeValue < 1) { controller.ReturnToBaseState(); return; }

        charges.RuntimeValue -= 1;

        Reload();

        controller = Controller.instance;

        controller.suitAnimator.SetBool("Dashing", true);
        controller.invulnerable = true;

        if (mouseDirection) controller.transform.forward = (MathExtension.MouseWorldPosition("Floor") - controller.transform.position).normalized;

        controller.rigidBody.velocity = controller.transform.forward * dashSpeed;

        StartCoroutine(EndDash());
    }

    public override void Exit()
    {
        controller.suitAnimator.SetBool("Dashing", false);
        controller.invulnerable = false;

        controller.rigidBody.velocity = Vector3.zero;
    }

    public IEnumerator EndDash()
    {
        yield return new WaitForSeconds(dashDuration);

        controller.ReturnToBaseState();
    }

    public void Reload()
    {
        if (reloading) return;

        reloading = true;

        CooldownManager.instance.SetCooldownAction(dashReloadTime.value, GainCharge);
    }

    public void GainCharge()
    {
        if (charges.RuntimeValue < Maxcharges.RuntimeValue) charges.RuntimeValue += 1;

        reloading = false;

        if (charges.RuntimeValue < Maxcharges.RuntimeValue) Reload();
    }
}
