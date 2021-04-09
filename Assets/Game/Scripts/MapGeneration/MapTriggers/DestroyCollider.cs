using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    private MapManager _mapManager;

    private void Start()
    {
        _mapManager = GetComponentInParent<MapManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _mapManager != null)
        {
            Destroy(gameObject);
            // _mapManager.DestroyPreviousMap();
        }
    }
}