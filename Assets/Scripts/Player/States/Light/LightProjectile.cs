using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProjectile : MonoBehaviour
{
    public GameObject particles;

    public float speed = 150f;
    public float destroyTime = 0.4f;

    public int damage = 1;

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
        if (other.CompareTag("Monster")) other.GetComponent<Enemy>().OnHit(damage);

        Instantiate(particles, transform.position, Quaternion.identity);
        CooldownManager.instance.StopTime();
        CooldownManager.instance.SetTime();
    }
}
