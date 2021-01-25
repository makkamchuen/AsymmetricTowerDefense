using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ATD.Dev
{
    public class GizmoNavMeshAgent : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, GetComponent<NavMeshAgent>().radius);
        }
    }
}