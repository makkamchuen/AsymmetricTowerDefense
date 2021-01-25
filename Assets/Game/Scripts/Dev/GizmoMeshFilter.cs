using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATD.Dev
{
    public class GizmoMeshFilter : MonoBehaviour
    {
        MeshFilter meshFilter;
        void OnDrawGizmos()
        {
            meshFilter = GetComponent<MeshFilter>();
            Gizmos.color = Color.blue;
            Gizmos.DrawWireMesh(meshFilter.sharedMesh, transform.position, transform.rotation, transform.lossyScale);
        }
    }
}