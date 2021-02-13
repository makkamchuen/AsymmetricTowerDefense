using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MapGenerator
{
    [SerializeField, Range(0,100)] private int randomFillPercent;
    
    public override void GenerateMap(int width, int height, System.Random pseudoRandom)
    {
        _mapManager.map = new int[width,height];
        for (int x = 0; x < width; x += 1) 
        {
            for (int y = 0; y < height; y += 1) 
            {
                if (x == 0 || x == width-1 || y == 0 || y == height -1) 
                {
                    _mapManager.map[x,y] = 1;
                }
                else {
                    _mapManager.map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
                }
            }
        }
    }
}
