﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATD.Dev
{
    public class GizmoMeshFilter : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireMesh(GetComponent<MeshFilter>().sharedMesh, transform.position, Quaternion.identity, transform.lossyScale);
        }
    }
}