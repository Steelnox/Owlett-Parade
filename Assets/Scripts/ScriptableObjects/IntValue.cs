using UnityEngine;
using System;

[CreateAssetMenu(fileName = "IntValue")]
[Serializable]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChange;

    private int runtimeValue;
    public int RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnValueChange?.Invoke(); } }

    [SerializeField] private int value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}
