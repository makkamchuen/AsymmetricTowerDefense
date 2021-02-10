using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{

    public int forestSizeX = 25; // Overall size of the forest (a square of forestSize X forestSize).
    public int forestSizeZ = 25;
    public int xOffset = 0;
    public int zOffset = 0;
    public int elementSpacing = 3; // The spacing between element placements. Basically grid size.
    [Range(0.1f, 3)]
    public float density = 1;

    public Element[] elements;

    private void Start()
    {
        // Loop through all the positions within our forest boundary.
        for (int x = 0; x < forestSizeX; x += elementSpacing)
        {
            for (int z = 0; z < forestSizeZ; z += elementSpacing)
            {
                // For each position, loop through each element...
                for (int i = 0; i < elements.Length; i++)
                {
                    // Get the current element.
                    Element element = elements[i];
                    {
                        // Check if the element can be placed.
                        if (element.CanPlace())
                        {
                            // Add random elements to element placement.
                            Vector3 position = new Vector3((x / density + xOffset), 0f, (z / density + zOffset));
                            Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
                            Vector3 rotation = new Vector3(Random.Range(0, 5f), Random.Range(0, 360f), Random.Range(0, 5f));
                            Vector3 scale = transform.lossyScale * Random.Range(0.9f, 1.1f);

                            // Instantiate and place element in world.
                            GameObject newElement = Instantiate(element.GetRandom());
                            newElement.transform.SetParent(transform);
                            newElement.transform.position = position + offset;
                            // newElement.transform.eulerAngles = rotation;
                            newElement.transform.localScale = scale;

                            // Break out of this for loop to ensure we don't place another element at this position.
                            break;
                        }
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class Element
{
    public string name;
    [Range(1, 10)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace()
    {
        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 10) < density)
            return true;
        else
            return false;
    }

    public GameObject GetRandom()
    {
        // Return a random GameObject prefab from the prefabs array.

        return prefabs[Random.Range(0, prefabs.Length)];
    }
}