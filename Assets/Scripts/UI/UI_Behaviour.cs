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
    [SerializeField] private List<Image> EmptyArmorImages;

    [SerializeField] private IntValue lightSuitArmor;
    [SerializeField] private IntValue lightSuitMaxArmor;
    [SerializeField] private IntValue heavySuitArmor;
    [SerializeField] private IntValue heavySuitMaxArmor;
    [SerializeField] private IntValue ccSuitArmor;
    [SerializeField] private IntValue ccSuitMaxArmor;
    [SerializeField] private IntValue noSuitArmor;
    [SerializeField] private IntValue noSuitMaxArmor;

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
    [SerializeField] private IntValue noSuitHealth;
    [SerializeField] private IntValue noSuitMaxHealth;

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
        UpdateChamberSuit();
    }

    private void SetSuit()
    {
        if (suitHealth) suitHealth.OnValueChange -= UpdateSuitHealth;
        if (armor) armor.OnValueChange -= UpdateSuitHealth;

        switch (currentSuit.RuntimeValue.suitType)
        {
            case SuitType.LIGHT:
                suitHealth = lightSuitHealth;
                suitMaxHealth = lightSuitMaxHealth;
                armor = lightSuitArmor;
                maxArmor = lightSuitMaxArmor;
                break;
            case SuitType.HEAVY:
                suitHealth = heavySuitHealth;
                suitMaxHealth = heavySuitMaxHealth;
                armor = heavySuitArmor;
                maxArmor = heavySuitMaxArmor;
                break;
            case SuitType.CC:
                suitHealth = ccSuitHealth;
                suitMaxHealth = ccSuitMaxHealth;
                armor = ccSuitArmor;
                maxArmor = ccSuitArmor;
                break;
            case SuitType.NONE:
                suitHealth = noSuitHealth;
                suitMaxHealth = noSuitMaxHealth;
                armor = noSuitArmor;
                maxArmor = noSuitMaxArmor;
                break;
        }

        suitHealth.OnValueChange += UpdateSuitHealth;
        armor.OnValueChange += UpdatePlayerHealth;

        UpdatePlayerHealth();
        UpdateChamberSuit();

        for (int i = EmptyArmorImages.Count - 1; i >= 0; i--)
        {
            bool active = i + 1 > maxArmor.RuntimeValue ? false : true;

            EmptyArmorImages[i].enabled = active;
            ArmorImages[i].enabled = active;
        }
    }

    public void UpdatePlayerHealth()
    {
        if (maxArmor.RuntimeValue == 0)
        {
            for (int i = 0; i < ArmorImages.Count; i++)
            {
                ArmorImages[i].enabled = false;
            }
        }

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

        switch (chamberSuit.RuntimeValue.suitType)
        {
            case SuitType.NONE:
                imageColor = Color.black;
                break;
            case SuitType.LIGHT:
                imageColor = Color.blue;
                break;
            case SuitType.HEAVY:
                imageColor = Color.red;
                break;
            case SuitType.CC:
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
