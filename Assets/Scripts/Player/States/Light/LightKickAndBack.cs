using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightKickAndBack : Skill
{
    public Transform spawnPosition;

    public float backRange;

    public override void Enter()
    {
        controller.transform.LookAt(MathExtension.MouseWorldPosition("Floor"));
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

        GetBack();
    }

    private void GetBack()
    {
        RaycastHit hit;
        Ray ray = new Ray(controller.transform.position, -controller.transform.forward);

        int layer_mask = LayerMask.GetMask("Default");

        Vector3 hitPoint = controller.transform.position + (-controller.transform.forward * backRange);

        if (Physics.Raycast(ray, out hit, backRange, layer_mask)) { hitPoint = hit.point + controller.transform.forward; }

        controller.transform.position = hitPoint;
    }
}
