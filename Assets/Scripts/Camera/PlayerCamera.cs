using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;

    private Vector3 desiredPosition;

    public float positionDamping;

    public Vector3 offset;

    private Vector3 velocity;

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        desiredPosition = target.transform.position;
        desiredPosition -= offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, positionDamping);
    }

    public void CenterToTarget()
    {
        float distance = (target.transform.position - transform.position).magnitude;

        transform.position = target.transform.position - transform.forward * distance;

        SetOffset();
    }

    public void SetOffset()
    {
        if (!target) return;

        offset = target.transform.position - transform.position;
    }
}
