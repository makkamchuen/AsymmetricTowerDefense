using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteCheckInObstacle : MonoBehaviour
{
    void Start()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}