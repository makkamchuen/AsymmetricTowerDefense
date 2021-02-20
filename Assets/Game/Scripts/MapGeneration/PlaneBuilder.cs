using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBuilder : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private int halfExtend;
    
    void Start()
    {
        Vector3 size = plane.GetComponentInChildren<SpriteRenderer>().bounds.size;
        for (float x = -size.x * halfExtend; x <= size.x * halfExtend; x += size.x)
        {
            for (float z = -size.z * halfExtend; z <= size.z * halfExtend; z += size.z)
            {
                GameObject newPlane = Instantiate(plane, transform.position + new Vector3(x, 0, z), Quaternion.identity);
                newPlane.transform.SetParent(transform);
            }
        }
    }
}
