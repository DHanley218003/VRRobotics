using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Vector3 targetPosition;

    void FixedUpdate()
    {
        targetPosition = transform.localPosition;
    }

    public Vector3 getTargetPosition()
    {
        return targetPosition;
    }
}
