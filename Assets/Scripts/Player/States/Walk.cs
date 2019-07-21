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

        walkSpeed = controller.currentSuit.moveSpeed;
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
        controller.rigidBody.velocity = Vector3.zero;
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

        if (Input.GetButtonDown("QuickDash") && controller.dash.GetComponent<Dash>().charges.RuntimeValue > 0)
        {
            controller.movement = Vector3.zero;
            controller.ChangeState(controller.dash);

            return;
        }
    }

    public void Move()
    {
        if (!controller) return;
        var targetSpeed = controller.movement.normalized * walkSpeed;

        targetSpeed.y = CheckStairs() ? 5 : CheckFloor() ? 0 : Physics.gravity.y;

        controller.rigidBody.velocity = Vector3.SmoothDamp(controller.rigidBody.velocity, targetSpeed, ref velocitySmoothing, smoothSpeedValue);

        Vector3 rotation = Vector3.RotateTowards(controller.transform.forward, controller.movement, rotationSpeed * Time.deltaTime, 0);

        controller.transform.forward = rotation;
    }

    public bool CheckFloor()
    {
        RaycastHit hit;
        Vector3 rayOrigin = new Vector3(controller.transform.position.x,
                                        controller.GetComponent<CapsuleCollider>().bounds.min.y,
                                        controller.transform.position.z);

        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (Physics.Raycast(ray, out hit, 0.2f, LayerMask.GetMask("Floor"))) return true;

        return false;
    }

    public bool CheckStairs()
    {
        RaycastHit hit;

        Vector3 rayOrigin = new Vector3(controller.transform.position.x,
                                        controller.GetComponent<CapsuleCollider>().bounds.min.y,
                                        controller.transform.position.z);

        Ray ray = new Ray(rayOrigin, Vector3.down);

        if (Physics.Raycast(ray, out hit, 0.2f))
        {
            if (hit.transform.tag == "Stair") return true;
        }

        return false;
    }
}
