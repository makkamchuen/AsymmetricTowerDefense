using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnvironmentGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float maxOffset;
    private List<GameObject> existingObstacles = new List<GameObject>();

    public void ResetEnvironment()
    {
        foreach (GameObject obstacle in existingObstacles)
        {
            Destroy(obstacle);
        }
    }
    
    public void BuildEnvironment(int[,] map, float unit, float centerX, float centerZ)
    {
        if (map != null)
        {
            int width = map.GetLength(0);
            int height = map.GetLength(1);
            for (int x = 0; x < width; x ++) 
            {
                for (int y = 0; y < height; y ++) 
                {
                    if (map[x, y] == 1)
                    {
                        Vector3 pos = new Vector3((-width / 2 + x) * unit + centerX, 0,
                            (-height / 2 + y) * unit + centerZ);
                        pos.x += Random.Range(-maxOffset, maxOffset);
                        pos.z += Random.Range(-maxOffset, maxOffset);
                        existingObstacles.Add(Instantiate(prefabs[0], pos, Quaternion.identity));
                    }
                }
            }
        }
    }
}
