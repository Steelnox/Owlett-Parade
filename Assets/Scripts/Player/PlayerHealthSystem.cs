using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthSystem : MonoBehaviour, IHealth
{
    [SerializeField] private Controller controller;

    [SerializeField] private IntValue maxHealth;
    [SerializeField] private IntValue currentHealth;

    public int MaxHealth { get => currentHealth.value; set => currentHealth.value = value; }
    public int CurrentHealth { get => currentHealth.value; set => currentHealth.value = value; }

    private bool dead = false;

    public void GetDamaged(int amount)
    {
        if (CurrentHealth <= 0 || controller.invulnerable) return;

        if (controller.currentSuit.suitType == Suit.SuitType.HEAVY) HeavyPassive.instance.LoseStack();
        if (controller.currentSuit) amount = controller.currentSuit.DecreaseArmor(amount);

        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth == 0) OutOfHealth();
        if (CurrentHealth == 1) StartCoroutine(controller.Invulnerable());
    }

    public void GetHealed(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    public void OutOfHealth()
    {
        if (dead) return;

        dead = true;

        print("She dead bro");
    }
}
