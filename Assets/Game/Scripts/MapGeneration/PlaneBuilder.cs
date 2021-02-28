using UnityEngine;

public class PlaneBuilder : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private int _halfExtend;
    private Vector3 size;

    private void Start()
    {
        size = plane.GetComponentInChildren<SpriteRenderer>().bounds.size;
    }

    public Vector3 GetSize()
    {
        return new Vector3(size.x * (_halfExtend * 2 + 1), 0, size.y * (_halfExtend * 2 + 1));
    }

    public void BuildPlane()
    {
        for (float x = -size.x * _halfExtend; x <= size.x * _halfExtend; x += size.x)
        {
            for (float z = -size.z * _halfExtend; z <= size.z * _halfExtend; z += size.z)
            {
                GameObject newPlane = Instantiate(plane, transform.position + new Vector3(x, 0, z), Quaternion.identity);
                newPlane.transform.SetParent(transform);
            }
        }
    }
}
