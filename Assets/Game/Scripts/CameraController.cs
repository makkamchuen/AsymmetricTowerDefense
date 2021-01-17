using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 _offset;

    public void Update()
    {
        transform.position = player.transform.position + _offset;
    }
}