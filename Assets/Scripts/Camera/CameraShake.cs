using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public static class CameraShake
{
    public static Transform camera;
    public static Vector3 startPosition = Vector3.zero;

    public static IEnumerator ShakeObject(float time, float force, UnityAction callback)
    {
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

        callback();
    }

    public static IEnumerator Blink(Transform objectModel, float timeBetweenBlink, float totalTime)
    {
        var actualTime = 0f;

        while (actualTime < totalTime)
        {
            objectModel.gameObject.SetActive(!objectModel.gameObject.activeSelf);
            yield return new WaitForSeconds(timeBetweenBlink);
            actualTime += timeBetweenBlink;
        }

        objectModel.gameObject.SetActive(true);
    }
}
