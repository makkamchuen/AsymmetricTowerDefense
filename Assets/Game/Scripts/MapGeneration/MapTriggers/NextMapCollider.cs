using UnityEngine;

public class NextMapCollider : MonoBehaviour
{
    private MapManager _mapManager;
    [SerializeField] private GameObject map;
    private Vector3 location;
    
    // Start is called before the first frame update
    private void Start()
    {
        _mapManager = GetComponentInParent<MapManager>();
        location = _mapManager.transform.position + new Vector3(_mapManager.GetSize().x, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            BuildNextMap();
        }
    }

    private void BuildNextMap()
    {
        if (_mapManager.mapNumber >= 4)
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
}
