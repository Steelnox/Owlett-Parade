using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : State
{
    private Controller controller;

    Vector3 velocitySmoothing;

    [Tooltip("Higher the value, slower the acceleration")]
    public float smoothSpeedValue = .1f;

    public float walkSpeed = 10f;
    public float rotationSpeed = 10f;

    public override void Enter()
    {
        controller = Controller.instance;

        controller.suitAnimator.SetBool("Moving", true);
    }

    public override void Execute()
    {
        Move();

        GetInput();
    }

    public override void Exit()
    {
        controller.movement = Vector3.zero;
        controller.suitAnimator.SetBool("Moving", false);
    }

    private void GetInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        controller.movement.x = x;
        controller.movement.y = 0;
        controller.movement.z = z;
        controller.movement.Normalize();

        if (x == 0 && z == 0)
        {
            controller.ChangeState(controller.idle);

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.movement = Vector3.zero;
            controller.ChangeState(controller.dash);

            return;
        }
    }

    public void Move()
    {
        if (!controller) return;
        var targetSpeed = controller.movement * walkSpeed;

        controller.rigidBody.velocity = Vector3.SmoothDamp(controller.rigidBody.velocity, targetSpeed, ref velocitySmoothing, smoothSpeedValue);

        Vector3 rotation = Vector3.RotateTowards(controller.transform.forward, controller.movement, rotationSpeed * Time.deltaTime, 0);

        controller.transform.forward = rotation;
    }
}
