using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    [NonSerialized] public int runtimeValue;

    public int value;

    public void OnAfterDeserialize() { runtimeValue = value; }

    public void OnBeforeSerialize() { }
}
