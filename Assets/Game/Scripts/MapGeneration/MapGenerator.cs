using System;
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

    public virtual void SetStartRow(int row)
    {
        throw new NotImplementedException();
    }
    
    public virtual Coordinate GetEndPoint()
    {
        throw new NotImplementedException();
    }
    
    public virtual Coordinate GetStartPoint()
    {
        throw new NotImplementedException();
    }
}
