using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FloatValue")]
[Serializable]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public delegate void OnValueChangeDelegate();
    public event OnValueChangeDelegate OnValueChange;

    [SerializeField] private float runtimeValue;
    public float RuntimeValue { get => runtimeValue; set { runtimeValue = value; OnValueChange?.Invoke(); } }

    [SerializeField] private float value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}
