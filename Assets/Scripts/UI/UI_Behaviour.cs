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
    public Image chamberSuit;

    public IntValue suitHealth;
    public IntValue suitMaxHealth;
    public Image suitHealthBar;
    public Text suitHealthText;

    [Header("Dash Gauge")]
    public IntValue dashCharges;
    public IntValue dashMaxCharges;

    public List<Image> dashGauge;


    private void Start()
    {
        SetListeners();

        UpdatePlayerHealth();
        UpdateSuitHealth();
        UpdateDashGauge();
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
        for (int i = dashMaxCharges.RuntimeValue - 1; i >= 0; i--)
        {
            dashGauge[i].enabled = i > dashCharges.RuntimeValue - 1 ? false : true;
        }
    }

    private void SetListeners()
    {
        health.OnValueChange += UpdatePlayerHealth;
        armor.OnValueChange += UpdatePlayerHealth;
        suitHealth.OnValueChange += UpdateSuitHealth;
        dashCharges.OnValueChange += UpdateDashGauge;
    }
}
