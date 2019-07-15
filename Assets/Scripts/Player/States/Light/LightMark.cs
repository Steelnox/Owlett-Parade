using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMark : MonoBehaviour
{
    public Enemy currentEnemy;

    private void Start()
    {
        transform.parent = currentEnemy.transform;
        transform.localPosition = Vector3.up * 1.5f;
    }
}
