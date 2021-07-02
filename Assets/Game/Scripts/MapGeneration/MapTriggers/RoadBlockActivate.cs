using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoadBlockActivate : MonoBehaviour
{
    private void Start()
    {
        // on game start, block character to walk left
        if (GetComponentInParent<MapManager>().mapNumber <= 1)
            RoadBlockEnable();

        // if it's on map 0, destroy
        if (this.transform.name == "RoadBlock 0")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && other.transform.position.x - transform.position.x > 0)
        {
            RoadBlockEnable();
        }
    }

    public void RoadBlockEnable()
    {
        GetComponent<NavMeshObstacle>().carving = true;
    }
}