using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeProjectile : BaseProjectile
{
    public float range = 3;
    public float explodeRange = 3;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        if ((transform.position - initialPosition).magnitude >= range) Explode();
    }

    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRange);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            var enemy = hitColliders[i].GetComponent<Enemy>();

            if (enemy) enemy.OnHit(damage);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == transform || other.tag == "Player") return;

        Explode();
    }
}
