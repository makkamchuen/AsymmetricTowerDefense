using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    // [SerializeField] private Mesh mesh;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireMesh(GetComponent<MeshFilter>().sharedMesh, transform.position, Quaternion.identity, transform.lossyScale);
    }
}