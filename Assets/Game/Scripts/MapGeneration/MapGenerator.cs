using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapGenerator: MonoBehaviour
{
    protected MapManager _mapManager;

    protected virtual void Start()
    {
        _mapManager = GetComponent<MapManager>();
        // if (_mapManager.mapGenerator == this)
        // {
        //     _mapManager.GenerateMap();
        // }
    }

    public abstract void GenerateMap(int width, int height, System.Random pseudoRandom);
}
