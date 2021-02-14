using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCheckInObstacle : MonoBehaviour
{
    private Collider[] hitColliders;
    private bool isInsideObstacle = false;

    void Start()
    {
        hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 10, Quaternion.identity, 1 << 8);
        
        if (hitColliders.Length == 0) { Destroy(gameObject); }
    }
}