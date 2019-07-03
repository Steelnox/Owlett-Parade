using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaRainAttack : Skill
{
    public GameObject prefab;

    public override void Enter()
    {
        GameObject attack = Instantiate(prefab, controller.transform.position, transform.rotation);

        controller.ChangeState(controller.idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
