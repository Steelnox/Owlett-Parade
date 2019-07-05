using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntValue")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChange;

    private int runtimeValue;
    public int RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnValueChange(); } }

    public int value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}

[CreateAssetMenu(fileName = "FloatValue")]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChange;

    private float runtimeValue;
    public float RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnValueChange(); } }

    public float value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}


[CreateAssetMenu(fileName = "SuitValue")]
public class SuitValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChange;

    private Suit runtimeValue;
    public Suit RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnValueChange(); } }

    public Suit value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}