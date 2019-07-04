using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitHealthSystem : MonoBehaviour, IHealth
{
    [SerializeField] public IntValue maxHealth;
    [SerializeField] public IntValue currentHealth;

    public int MaxHealth { get => maxHealth.RuntimeValue; set => maxHealth.RuntimeValue = value; }
    public int CurrentHealth { get => currentHealth.RuntimeValue; set => currentHealth.RuntimeValue = value; }

    [SerializeField] private Controller controller => Controller.instance;

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
        controller.currentSuit = null;

        if (controller.chamberSuit != null)
        {
            controller.chamberSuit.EquipSuit();
            controller.chamberSuit = null;
        }

        gameObject.SetActive(false);
    }
}
