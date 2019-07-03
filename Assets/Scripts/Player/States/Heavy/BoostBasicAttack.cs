using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBasicAttack : Skill
{
    public ParticleSystem particles;

    public float timeBeforeCooldown = 3;

    public bool boosting = true;

    public override void Enter()
    {
        available = false;
        boosting = true;

        particles.Play();
        controller.ChangeState(controller.idle);
    }

    public override void Exit()
    {
        StartCoroutine(MaxActiveTime());
    }

    public IEnumerator MaxActiveTime()
    {
        yield return new WaitForSeconds(timeBeforeCooldown);

        EndBoost();
    }

    public void EndBoost()
    {
        available = false;
        boosting = false;

        particles.Stop();
        controller.cooldownManager.SetCooldown(this);
    }
}
