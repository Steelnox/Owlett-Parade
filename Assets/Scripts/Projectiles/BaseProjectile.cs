using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : MonoBehaviour
{
    public float speed;
    public int damage;

    public void SetStats(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
}
