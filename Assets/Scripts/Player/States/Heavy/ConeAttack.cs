using UnityEngine;
using System.Collections;

public class ConeAttack : Skill
{
    public GameObject effects;
    public Transform spawnPosition;

    public override void Enter()
    {
        controller.transform.LookAt(MathExtension.MouseWorldPosition("Floor"));
        controller.suitAnimator.SetTrigger("Attack");
    }

    public void OnAttack()
    {
        if (controller.currentState != this) return;

        GameObject attack = Instantiate(effects, spawnPosition.position, transform.rotation);

        attack.GetComponentInChildren<OnParticleTrigger>().damage = damage + HeavyPassive.instance.stacks;
    }
}
