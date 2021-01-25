using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoSphere : MonoBehaviour
{

    SphereCollider sphere;
    Vector3 spherePos;

    void OnDrawGizmos()
    {
        sphere = GetComponent<SphereCollider>();
        spherePos = new Vector3(sphere.transform.position.x + sphere.center.x, sphere.transform.position.y + sphere.center.y, sphere.transform.position.z + sphere.center.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphere.radius);
    }

    /* Failed attempt to make it change color with drop down menu
    public enum Colors { Red, Green, Blue, Yellow, White, Black }

    [SerializeField] Colors color;

    Colors colorChoice;

        switch (colorChoice)
        {
            case Colors.Red:
                Gizmos.color = Color.red;
                break;
            case Colors.Green:
                Gizmos.color = Color.green;
                break;
            case Colors.Blue:
                Gizmos.color = Color.blue;
                break;
            case Colors.Yellow:
                Gizmos.color = Color.yellow;
                break;
            case Colors.White:
                Gizmos.color = Color.white;
                break;
            case Colors.Black:
                Gizmos.color = Color.black;
                break;
            default:
                Gizmos.color = Color.blue;
                break;
        }
        */
}