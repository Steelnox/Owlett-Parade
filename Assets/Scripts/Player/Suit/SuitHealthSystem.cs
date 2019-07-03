using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitHealthSystem : MonoBehaviour, IHealth
{
    [SerializeField] private IntValue maxHealth;
    [SerializeField] private IntValue currentHealth;

    public int MaxHealth { get => maxHealth.value; set => maxHealth.value = value; }
    public int CurrentHealth { get => currentHealth.value; set => currentHealth.value = value; }

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
