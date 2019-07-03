using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    #region Singleton

    public static CooldownManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);
    }

    #endregion

    public void SetCooldown(Skill skill)
    {
        StartCoroutine(Cooldown(skill));
    }

    public IEnumerator Cooldown(Skill skill)
    {
        yield return new WaitForSeconds(skill.cooldown);

        FinishCooldown(skill);
    }

    public void FinishCooldown(Skill skill)
    {
        skill.available = true;
    }

    public void SetTime()
    {
        StartCoroutine(SlowTime());
    }

    public void StopTime()
    {
        StopCoroutine(SlowTime());

        Time.timeScale = 1;
    }

    public IEnumerator SlowTime()
    {
        Time.timeScale = 0.75f;

        yield return new WaitForSecondsRealtime(0.05f);

        Time.timeScale = 0.25f;

        yield return new WaitForSecondsRealtime(0.05f);

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(0.05f);

        Time.timeScale = .25f;

        yield return new WaitForSecondsRealtime(0.05f);

        Time.timeScale = 0.75f;

        yield return new WaitForSecondsRealtime(0.05f);

        Time.timeScale = 1;
    }
}
