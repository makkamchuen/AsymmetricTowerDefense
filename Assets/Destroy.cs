using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destroy : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "WorldBoundary") { Destroy(gameObject); }
    }
}