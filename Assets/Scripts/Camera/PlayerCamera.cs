
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public Transform target;

    private Vector3 desiredPosition;

    public float positionDamping;

    public Vector3 offset;
    private Vector3 velocity;

    public Transform camera;
    public Vector3 startPosition = Vector3.zero;

    public Transform newTarget;

    private void Awake()
    {
        if (instance == null) instance = this;
        if (instance != this) Destroy(this);

        camera = transform.GetChild(0);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(ShakeObject(.15f, .3f, newTarget));
        }
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

    public IEnumerator ShakeObject(float time, float force, Transform targetDirection)
    {
        positionDamping = .7f;
        target = targetDirection;

        float timer = 0f;

        while (timer < time)
        {
            Vector3 unitSphere = Random.insideUnitSphere;
            unitSphere.z = 0;
            camera.localPosition = startPosition + (unitSphere.normalized * force);
            yield return null;
            timer += Time.deltaTime;
        }

        camera.localPosition = startPosition;

        positionDamping = .15f;
        target = Controller.instance.transform;

    }
}
