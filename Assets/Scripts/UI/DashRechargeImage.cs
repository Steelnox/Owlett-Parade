using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DashRechargeImage : MonoBehaviour
{
    public Image image;

    public FloatValue reloadTime;
    public float timer = 0f;

    public bool reloading = false;

    void Update()
    {
        if (!reloading) return;

        timer += Time.deltaTime;

        image.fillAmount = Mathf.Clamp(timer / reloadTime.RuntimeValue, 0, 1);

        if (timer >= reloadTime.RuntimeValue)
        {
            timer = 0;
            reloading = false;
        }
    }

    public void Reloading(bool reload, bool enableImage)
    {
        timer = 0;

        reloading = reload;
        image.fillAmount = reload ? 0 : 1;
        image.enabled = enableImage;
    }
}
