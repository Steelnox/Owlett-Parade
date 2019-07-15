using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtension
{
    public static Vector3 MouseWorldPosition(string layerMaskName)
    {
        Vector3 mousePosition = Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layer_mask = LayerMask.GetMask(layerMaskName);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer_mask))
        {
            mousePosition = hit.point;
        }

        return mousePosition;
    }

    public static Vector3 ForwardWithoutY(Transform from, Vector3 to)
    {
        to.y = from.position.y;

        return (to - from.position).normalized;
    }
}
