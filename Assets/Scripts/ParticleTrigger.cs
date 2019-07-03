using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem particles;
    public Renderer particleRenderer;
    public BoxCollider boxCollider;

    void Update()
    {
        Vector3 direction = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0) * transform.forward;
        float distance = (particleRenderer.bounds.center - transform.position).magnitude;

        boxCollider.transform.localPosition = direction * distance;

        if (particles.time > particles.main.duration * 0.66f)
        {
            float x = particleRenderer.bounds.size.x > particleRenderer.bounds.size.z ?
                      particleRenderer.bounds.size.x : particleRenderer.bounds.size.z;
            float z = particleRenderer.bounds.size.x < particleRenderer.bounds.size.z ?
                      particleRenderer.bounds.size.x : particleRenderer.bounds.size.z;

            if (particleRenderer.bounds.size.x < particleRenderer.bounds.size.z) z /= 2f;

            boxCollider.size = new Vector3(x, particleRenderer.bounds.size.y, z);
        }
        else
        {
            boxCollider.size = particleRenderer.bounds.size;
        }
    }
}
