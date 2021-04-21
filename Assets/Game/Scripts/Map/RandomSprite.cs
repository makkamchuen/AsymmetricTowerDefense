using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<GameObject> spriteList = new List<GameObject>();

    public Element[] elements;
    private bool flip = false;

    private List<int> prefixSums = new List<int>();

    private void Start()
    {
        foreach (Element el in elements)
        {
            if (prefixSums.Count == 0)
                prefixSums.Add(el.density);
            else
                prefixSums.Add(prefixSums[prefixSums.Count - 1] + el.density);
        }
    }

    int pickIndex()
    {
        // generate a random number in the range of [0, 1]
        float target = Random.Range(0, prefixSums[prefixSums.Count - 1]);
        // run a linear search to find the target zone
        for (int i = 0; i < prefixSums.Count(); ++i)
            if (target < prefixSums[i])
                return i;
        return prefixSums.Count() - 1;
    }

    public void PlaceSprite()
    {
        // GameObject[] spriteList = GetComponent<MapManager>().spriteList;
        // Loop through all the positions within our forest boundary.
        for (int x = 0; x < forestSizeX; x += elementSpacing)
        {
            for (int z = 0; z < forestSizeZ; z += elementSpacing)
            {
                // Get the current element.
                Element element = elements[pickIndex()];

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
                spriteList.Add(newElement);
            }
        }
    }

    public void UpdateSprite()
    {
        spriteList = spriteList.OrderBy(abc => System.Guid.NewGuid()).ToList();
        // Loop through all the positions within our forest boundary.
        int i = 0;
        for (int x = 0; x < forestSizeX; x += elementSpacing)
        {
            for (int z = 0; z < forestSizeZ; z += elementSpacing)
            {
                //if (i >= spriteList.Count) return;

                GameObject savedSprite = spriteList[i++];
                // Add random elements to element placement.
                Vector3 position = new Vector3((forestSizeX / 2 + x) / generalDensity + transform.position.x + xOffset, 0f, (forestSizeZ / 2 + z) / generalDensity + transform.position.z + zOffset);
                Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
                // Vector3 rotation = new Vector3(45f, Random.Range(0, 360f), Range(0, 1f));
                Vector3 scale = transform.localScale * Random.Range(0.8f, 1.2f);

                savedSprite.transform.position = position + offset;

                if (savedSprite.GetComponent<SpriteCheckInObstacle>().CheckOnRoad() == false)
                {
                    savedSprite.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    savedSprite.transform.localScale = scale;
                    if (Random.value >.5f) { savedSprite.GetComponentInChildren<SpriteRenderer>().flipX = true; }
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