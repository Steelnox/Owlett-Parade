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
        if (!controller) controller = Controller.instance;
        controller.suitAnimator.SetBool("Dashing", true);

        colliderTrigger.isTrigger = true;

        if (mouseDirection) controller.transform.forward = (MathExtension.MouseWorldPosition("Floor") - controller.transform.position).normalized;

        controller.rigidBody.velocity = controller.transform.forward * dashSpeed;
    }

    public override void Execute()
    {
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

        brokeMark = false;
        controller.suitAnimator.SetBool("Dashing", false);
        controller.rigidBody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.OnHit(damage);

            brokeMark = LightPassive.instance.BreakMark(enemy);
        }
    }
}
