using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSuitBasicAttack : Skill
{
    public Transform spawnPosition;

    public override void Enter()
    {
        controller.transform.forward = MathExtension.ForwardWithoutY(transform, MathExtension.MouseWorldPosition("Floor"));
        controller.suitAnimator.SetTrigger("Attack");
    }

    public override void Execute()
    {
        controller.rigidBody.velocity = Vector3.zero;
    }

    public void OnAttack()
    {
        if (controller.currentState != this) return;

        Collider[] hits = Physics.OverlapSphere(spawnPosition.position, 2.5f);

        for (int i = 0; i < hits.Length; i++)
        {
            var enemy = hits[i].GetComponent<Enemy>();

            if (enemy) enemy.OnHit(damage);
        }

    }
}
