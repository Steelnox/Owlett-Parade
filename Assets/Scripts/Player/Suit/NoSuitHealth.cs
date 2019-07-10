using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSuitHealth : MonoBehaviour, IHealth
{
    [SerializeField] public IntValue maxHealth;
    [SerializeField] public IntValue currentHealth;

    public int MaxHealth { get => maxHealth.RuntimeValue; set => maxHealth.RuntimeValue = value; }
    public int CurrentHealth { get => currentHealth.RuntimeValue; set => currentHealth.RuntimeValue = value; }

    public void GetDamaged(int amount) { }

    public void GetHealed(int amount) { }

    public void OutOfHealth() { }
}
