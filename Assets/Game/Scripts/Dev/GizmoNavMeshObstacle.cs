using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ATD.Dev
{
    public class GizmoNavMeshObstacle : MonoBehaviour
    {
        NavMeshObstacle NMO;
        Vector3 nmoPos;

        void OnDrawGizmos()
        {
            NMO = GetComponent<NavMeshObstacle>();
            nmoPos = new Vector3(NMO.transform.position.x + NMO.center.x, NMO.transform.position.y + NMO.center.y, NMO.transform.position.z + NMO.center.z);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(nmoPos, NMO.size);
        }
    }
}