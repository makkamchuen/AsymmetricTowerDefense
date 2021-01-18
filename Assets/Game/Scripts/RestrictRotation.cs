using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictRotation : MonoBehaviour
{
    void Update()
    {
        RestrictRotate();
    }

    public void RestrictRotate()
    {
        Quaternion newTransformRotation = transform.rotation;
        transform.rotation = new Quaternion(
            newTransformRotation.x,
            0f,
            newTransformRotation.z,
            newTransformRotation.w
        );
    }
}