using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATD.Dev
{
    public class GizmoBox : MonoBehaviour
    {
        public ColorChoice dropDown = new ColorChoice();

        public enum ColorChoice
        {
            Red,
            Green,
            Blue,
            Yellow,
            White,
            Black,
        }

        Vector3 boxSize;
        void OnDrawGizmos()
        {
            boxSize = GetComponent<BoxCollider>().size;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }
}