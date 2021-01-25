using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATD.Dev
{
    public class GizmoBox : MonoBehaviour
    {
        BoxCollider box;
        Vector3 boxPos;
        void OnDrawGizmos()
        {
            box = GetComponent<BoxCollider>();
            boxPos = new Vector3(box.transform.position.x + box.center.x, box.transform.position.y + box.center.y, box.transform.position.z + box.center.z);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(boxPos, box.size);
        }
    }
}