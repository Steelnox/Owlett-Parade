using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Behaviour : MonoBehaviour
{
    [Header("Health & Armor")]
    public IntValue health;
    public IntValue maxHealth;
    public List<Image> HealthImages;

    public IntValue armor;
    public IntValue maxArmor;
    public List<Image> ArmorImages;

    [Header("Suit Health & Chamber")]
    public Suit currentSuit;
    public Suit chamberSuit;
    public Image chamberSuitImage;

    public IntValue lightSuitHealth;
    public IntValue lightSuitMaxHealth;
    public IntValue heavySuitHealth;
    public IntValue heavySuitMaxHealth;
    public IntValue ccSuitHealth;
    public IntValue ccSuitMaxHealth;

    public IntValue suitHealth;
    public IntValue suitMaxHealth;

    public Image suitHealthBar;
    public Text suitHealthText;

    [Header("Dash Gauge")]
    public IntValue dashCharges;
    public IntValue dashMaxCharges;

    public List<DashRechargeImage> dashGauge;

    private Controller controller;

    private void Start()
    {
        controller = Controller.instance;

        SetSuit();
        SetListeners();

        UpdatePlayerHealth();
        UpdateSuitHealth();
        UpdateDashGauge();
    }

    private void SetSuit()
    {
        if (suitHealth) suitHealth.OnValueChange -= UpdateSuitHealth;

        currentSuit = controller.currentSuit;
        chamberSuit = controller.chamberSuit;

        switch (currentSuit.suitType)
        {
            case Suit.SuitType.LIGHT:
                suitHealth = lightSuitHealth;
                suitMaxHealth = lightSuitMaxHealth;
                break;
            case Suit.SuitType.HEAVY:
                suitHealth = heavySuitHealth;
                suitMaxHealth = heavySuitMaxHealth;
                break;
            case Suit.SuitType.CC:
                suitHealth = ccSuitHealth;
                suitMaxHealth = ccSuitMaxHealth;
                break;
        }

        suitHealth.OnValueChange += UpdateSuitHealth;
    }

    public void UpdatePlayerHealth()
    {
        for (int i = maxArmor.RuntimeValue - 1; i >= 0; i--)
        {
            ArmorImages[i].enabled = i > armor.RuntimeValue - 1 ? false : true;
        }

        for (int i = maxHealth.RuntimeValue - 1; i >= 0; i--)
        {
            HealthImages[i].enabled = i > health.RuntimeValue - 1 ? false : true;
        }
    }

    public void UpdateSuitHealth()
    {
        suitHealthBar.fillAmount = (float)suitHealth.RuntimeValue / (float)suitMaxHealth.RuntimeValue;
        suitHealthText.text = suitHealth.RuntimeValue + " / " + suitMaxHealth.RuntimeValue;
    }

    public void UpdateDashGauge()
    {
        float rechargingAmount = 0f;

        foreach (DashRechargeImage recharge in dashGauge)
        {
            if (recharge.reloading)
            {
                rechargingAmount = recharge.timer;
            }

            recharge.Reloading(false, recharge.image.enabled);
        }

        for (int i = dashMaxCharges.RuntimeValue - 1; i >= 0; i--)
        {
            dashGauge[i].image.enabled = i > dashCharges.RuntimeValue - 1 ? false : true;
        }

        for (int i = 0; i < dashGauge.Count; i++)
        {
            if (!dashGauge[i].image.enabled)
            {
                dashGauge[i].Reloading(true, true);
                dashGauge[i].timer = rechargingAmount;
                return;
            }
        }
    }

    public void UpdateChamberSuit()
    {
        Color imageColor = Color.black;

        if (!chamberSuit) return;

        switch (chamberSuit.suitType)
        {
            case Suit.SuitType.NONE:
                imageColor = Color.black;
                break;
            case Suit.SuitType.LIGHT:
                imageColor = Color.blue;
                break;
            case Suit.SuitType.HEAVY:
                imageColor = Color.red;
                break;
            case Suit.SuitType.CC:
                imageColor = Color.green;
                break;
        }

        chamberSuitImage.color = imageColor;
    }

    private void SetListeners()
    {
        health.OnValueChange += UpdatePlayerHealth;
        armor.OnValueChange += UpdatePlayerHealth;
        dashCharges.OnValueChange += UpdateDashGauge;
        suitHealth.OnValueChange += UpdateSuitHealth;
        controller.OnSuitChange += SetSuit;
        controller.OnSuitChange += UpdateChamberSuit;
    }
}
