using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : Skill
{
    public float speed;

    public GameObject effects;
    public Transform spawnPosition;

    public override void Enter()
    {
        controller.transform.forward = MathExtension.ForwardWithoutY(transform, MathExtension.MouseWorldPosition("Floor"));
        controller.suitAnimator.SetTrigger("Attack");
    }

    public void OnAttack()
    {
        if (controller.currentState != this) return;

        GameObject attack = Instantiate(effects, spawnPosition.position, transform.rotation);

        //CleanUp
        if (attack.GetComponent<BaseProjectile>())
            attack.GetComponent<BaseProjectile>().SetStats(speed, damage);
    }
}
