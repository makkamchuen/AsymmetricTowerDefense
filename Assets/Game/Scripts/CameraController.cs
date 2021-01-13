using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private readonly Vector3 _offset = new Vector3(0, 1.5f, -2f);

    public void Update()
    {
        transform.position = player.transform.position + _offset;
    }
}