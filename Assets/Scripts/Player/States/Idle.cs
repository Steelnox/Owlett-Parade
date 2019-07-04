﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private Controller controller;

    public override void Enter()
    {
        controller = Controller.instance;
    }

    public override void Execute()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) controller.ChangeState(controller.walk);

        if (Input.GetKeyDown(KeyCode.Space) && controller.dash.GetComponent<Dash>().charges.RuntimeValue > 0)
        {
            controller.ChangeState(controller.dash);

            return;
        }
    }

    public override void Exit() { }
}