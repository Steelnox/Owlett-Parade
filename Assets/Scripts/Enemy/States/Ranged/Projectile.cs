using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform myTransform;
    private Controller player;
    public float ProjectileSpeed;
    public int dmg;

    private float timer;
    public float deathTime;

    public bool activated;

    public Vector3 startPosition;

    void Start()
    {
        player = Controller.instance;

        timer = 0;
        myTransform = transform;

        startPosition = transform.position;

        activated = false;

    }

    void Update()
    {
        if (activated)
        {
            timer += Time.deltaTime;

            float Move = ProjectileSpeed * Time.deltaTime;
            myTransform.Translate(Vector3.forward * Move);

            if (timer >= deathTime)
            {
                timer = 0;
                activated = false;
            }
        }


        else
        {
            transform.position = startPosition;
        }
    }

}
