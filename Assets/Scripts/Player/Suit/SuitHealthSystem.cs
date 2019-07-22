using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitHealthSystem : MonoBehaviour, IHealth
{
    [SerializeField] public IntValue maxHealth;
    [SerializeField] public IntValue currentHealth;

    public int MaxHealth { get => maxHealth.RuntimeValue; set => maxHealth.RuntimeValue = value; }
    public int CurrentHealth { get => currentHealth.RuntimeValue; set => currentHealth.RuntimeValue = value; }

    [SerializeField] private Controller Controller => Controller.instance;

    public void GetDamaged(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth == 0) OutOfHealth();
    }

    public void GetHealed(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    public void OutOfHealth()
    {
        Controller.currentSuit = null;

        if (Controller.chamberSuit != null)
        {
            Controller.chamberSuit.EquipSuit();
            Controller.chamberSuit = null;
            Controller.chamberSuitValue.RuntimeValue = Controller.noSuit;
        }
        else
        {
            Controller.noSuit.EquipSuit();
        }

        gameObject.SetActive(false);
    }
}
