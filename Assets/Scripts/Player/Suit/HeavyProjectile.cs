using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyProjectile : BaseProjectile
{
    public float explodeRange;

    public GameObject explodeParticles;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Explode()
    {
        if (explodeParticles) Instantiate(explodeParticles, transform.position, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRange);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            var enemy = hitColliders[i].GetComponent<Enemy>();

            if (enemy) enemy.OnHit(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") return;

        PlayerCamera.instance.StartCoroutine(PlayerCamera.instance.ShakeObject(0.1f, 0.3f, other.transform));
        CooldownManager.instance.StopTime();
        CooldownManager.instance.SetTime();

        Explode();
        Destroy(gameObject);
    }
}
