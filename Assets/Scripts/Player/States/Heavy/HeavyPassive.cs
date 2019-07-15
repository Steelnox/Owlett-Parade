using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyPassive : MonoBehaviour
{
    public static HeavyPassive instance;

    public int stacks = 0;
    public int maxStacks = 5;

    public float explodeCooldown = 3f;
    public float explodeRadius = 3f;
    public int explodeDamage = 3;

    public float timeBeforeLoseStack;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
    }

    public void GetStack()
    {
        stacks = Mathf.Clamp(stacks + 1, 0, maxStacks);

        StartCoroutine(ExplodeSuit());

        CancelInvoke("LoseStack()");
        Invoke("LoseStack()", timeBeforeLoseStack);
    }

    public void LoseStack()
    {
        stacks = Mathf.Clamp(stacks - 1, 0, 2);
    }

    public IEnumerator ExplodeSuit()
    {
        while (stacks == maxStacks && IsSuitOn())
        {
            Explosion();

            yield return new WaitForSeconds(explodeCooldown);
        }
    }

    private void Explosion()
    {
        var explosion = Physics.OverlapSphere(transform.position, explodeRadius, LayerMask.GetMask("Enemy"));

        for (int i = 0; i < explosion.Length; i++)
        {
            var enemy = explosion[i].GetComponent<Enemy>();

            if (enemy) enemy.OnHit(explodeDamage);
        }
    }

    private bool IsSuitOn()
    {
        Controller controller = Controller.instance;

        if (controller.currentSuit.suitType == SuitType.HEAVY) return true;
        if (controller.chamberSuit && controller.chamberSuit.suitType == SuitType.HEAVY) return true;

        return false;
    }
}
