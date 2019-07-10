using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Behaviour : MonoBehaviour
{
    #region Variables

    [Header("Health & Armor")]

    [SerializeField] private IntValue health;
    [SerializeField] private IntValue maxHealth;
    [SerializeField] private List<Image> HealthImages;

    [SerializeField] private IntValue armor;
    [SerializeField] private IntValue maxArmor;
    [SerializeField] private List<Image> ArmorImages;

    [Space]
    [Header("Suit Health & Chamber")]
    [Space]

    [SerializeField] private SuitValue currentSuit;
    [SerializeField] private SuitValue chamberSuit;

    [SerializeField] private Image chamberSuitImage;

    [Space]

    [SerializeField] private IntValue lightSuitHealth;
    [SerializeField] private IntValue lightSuitMaxHealth;
    [SerializeField] private IntValue heavySuitHealth;
    [SerializeField] private IntValue heavySuitMaxHealth;
    [SerializeField] private IntValue ccSuitHealth;
    [SerializeField] private IntValue ccSuitMaxHealth;

    private IntValue suitHealth;
    private IntValue suitMaxHealth;

    [Space]

    [SerializeField] private Image suitHealthBar;
    [SerializeField] private Text suitHealthText;

    [Space]
    [Header("Dash Gauge")]
    [Space]

    [SerializeField] private IntValue dashCharges;
    [SerializeField] private IntValue dashMaxCharges;

    [SerializeField] private List<DashRechargeImage> dashGauge;

    #endregion

    private void Start()
    {
        SetSuit();
        SetListeners();

        UpdatePlayerHealth();
        UpdateSuitHealth();
        UpdateDashGauge();
    }

    private void SetSuit()
    {
        if (suitHealth) suitHealth.OnValueChange -= UpdateSuitHealth;

        switch (currentSuit.RuntimeValue.suitType)
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

        switch (chamberSuit.RuntimeValue.suitType)
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
        currentSuit.OnValueChange += SetSuit;
        currentSuit.OnValueChange += UpdateChamberSuit;
        chamberSuit.OnValueChange += SetSuit;
        chamberSuit.OnValueChange += UpdateChamberSuit;
    }
}
