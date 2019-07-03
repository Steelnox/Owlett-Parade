using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class IntValue : ScriptableObject, ISerializationCallbackReceiver
{
    [NonSerialized] public int value;

    [SerializeField] private bool resetAfterPlay;

    public int diskValue;

    public void OnAfterDeserialize() { if (resetAfterPlay) value = diskValue; }

    public void OnBeforeSerialize() { }
}
