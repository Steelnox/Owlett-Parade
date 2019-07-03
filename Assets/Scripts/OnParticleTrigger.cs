using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleTrigger : MonoBehaviour
{
    public List<GameObject> collided;
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!collided.Contains(other.gameObject))
        {
            Debug.Log(damage);
            collided.Add(other.gameObject);

            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            if (enemy) enemy.OnHit(damage);

        }
    }
}
