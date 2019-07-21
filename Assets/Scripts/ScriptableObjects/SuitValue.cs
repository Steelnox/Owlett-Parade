using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SuitValue")]
[Serializable]
public class SuitValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChange;

    private Suit runtimeValue;
    public Suit RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnValueChange?.Invoke(); } }

    [SerializeField] private Suit value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}