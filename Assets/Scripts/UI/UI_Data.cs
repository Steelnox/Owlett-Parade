using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnVariableChangeDelegate();
    public event OnVariableChangeDelegate OnVariableChange;

    private int runtimeValue;
    public int RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnVariableChange(); } }

    public int value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}
