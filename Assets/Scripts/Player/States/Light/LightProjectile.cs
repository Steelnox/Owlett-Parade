using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : BaseProjectile
{
    public GameObject particles;

    public float destroyTime = 0.4f;

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Floor")) return;
        if (other.CompareTag("Monster"))
        {
            StartCoroutine(PlayerCamera.instance.ShakeObject(0.1f, 0.3f, other.transform));
            other.GetComponent<Enemy>().OnHit(damage);
        }

        Instantiate(particles, transform.position, Quaternion.identity);
        CooldownManager.instance.StopTime();
        CooldownManager.instance.SetTime();
    }
}
