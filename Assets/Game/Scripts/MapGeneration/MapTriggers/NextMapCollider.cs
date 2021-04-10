using UnityEngine;

public class NextMapCollider : MonoBehaviour
{
    private MapManager mapManagerSelf;
    MapManager[] mapManagerPool;

    // Start is called before the first frame update
    private void Start()
    {
        mapManagerSelf = GetComponentInParent<MapManager>();
        // location = _mapManager.transform.position + new Vector3(_mapManager.GetSize().x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            var parentMapObject = mapManagerSelf.transform.parent.gameObject;
            mapManagerPool = parentMapObject.GetComponentsInChildren<MapManager>();

            foreach (MapManager mapManager in mapManagerPool)
            {
                if (mapManager.mapNumber == mapManagerSelf.mapNumber - 2)
                {
                    mapManager.GenerateMap();
                    mapManager.transform.position = mapManager.transform.position + new Vector3(76, 0, 0);
                    mapManager.mapNumber += 3;
                }
            }

            foreach (MapManager mapManager in mapManagerPool)
            {
                mapManager.RebakeNavMesh();
            }
        }
    }

    /* 
        private void BuildNextMap()
        {
            if (_mapManager.mapNumber >= 3)
            {
                return;
            }
            GameObject nextMap = Instantiate(
                map,
                location,
                Quaternion.identity,
                transform.parent.parent
            );
            nextMap.GetComponent<MapManager>().SetPreviousMap(_mapManager);
        }
         */
}