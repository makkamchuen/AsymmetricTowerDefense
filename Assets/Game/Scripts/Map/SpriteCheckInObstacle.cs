using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpriteCheckInObstacle : MonoBehaviour
{
    void Start()
    {
        CheckOnRoad();
    }

    public bool CheckOnRoad()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
        {
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            return true;
        }
        return false;
    }
}