using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomSprite : MonoBehaviour
{

    public int forestSizeX = 25; // Overall size of the forest (a square of forestSize X forestSize).
    public int forestSizeZ = 25;
    public int xOffset = 0;
    public int zOffset = 0;
    public int elementSpacing = 3; // The spacing between element placements. Basically grid size.
    [Range(0.1f, 3)]
    public float generalDensity = 1;

    public Element[] elements;
    private bool flip = false;

    private void Start()
    {

    }

    public void placeSprite()
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
                            Vector3 position = new Vector3((forestSizeX / 2 + x) / generalDensity + transform.position.x + xOffset, 0f, (forestSizeZ / 2 + z) / generalDensity + transform.position.z + zOffset);
                            Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
                            // Vector3 rotation = new Vector3(45f, Random.Range(0, 360f), Range(0, 1f));
                            Vector3 scale = transform.localScale * Random.Range(0.8f, 1.2f);

                            // Instantiate and place element in world.

                            // todo: put element into new array to keep track and re-place
                            GameObject newElement = Instantiate(element.GetRandom());
                            newElement.transform.SetParent(transform);
                            newElement.transform.position = position + offset;
                            newElement.transform.localScale = scale;
                            if (Random.value >.5f) { newElement.GetComponentInChildren<SpriteRenderer>().flipX = true; }

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
    [Range(0, 100)]
    public int density;

    public GameObject[] prefabs;

    public bool CanPlace()
    {
        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 100) < density)
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