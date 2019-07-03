using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyPassive : MonoBehaviour
{
    public static HeavyPassive instance;

    public int stacks = 0;

    public float timeBeforeLoseStack;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
    }

    public void GetStack()
    {
        stacks = Mathf.Clamp(stacks + 1, 0, 2);

        Invoke("LoseStack()", timeBeforeLoseStack);
    }

    public void LoseStack()
    {
        stacks = Mathf.Clamp(stacks - 1, 0, 2);
    }
}
