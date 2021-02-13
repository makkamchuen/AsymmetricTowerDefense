using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk : MapGenerator
{
    [SerializeField] private int tunnelCount;
    [SerializeField] private int minTunnelLength;
    [SerializeField] private int maxTunnelLength;
    
    public override void GenerateMap(int width, int height, System.Random pseudoRandom)
    {
        _mapManager.map = new int[width,height];
        _mapManager.FillMap(1);
        int x = pseudoRandom.Next(1, width - 1);
        int y = pseudoRandom.Next(1, height - 1);
        _mapManager.map[x, y] = 0;
        Tuple<int, int>[] directions = {new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 0), new Tuple<int, int>(0, -1), new Tuple<int, int>(-1, 0)};
        int direction = pseudoRandom.Next(0, 4);
        for (int tunnelRemain = tunnelCount; tunnelRemain > 0; tunnelRemain -= 1)
        {
            int length = pseudoRandom.Next(minTunnelLength, maxTunnelLength);
            int newX = x + directions[direction].Item1 * length;
            int newY = y + directions[direction].Item2 * length;
            newX = Mathf.Min(width - 2, newX);
            newY = Mathf.Min(height - 2, newY);
            newX = Mathf.Max(1, newX);
            newY = Mathf.Max(1, newY);
            if (newX == x && newY == y)
            {
                tunnelRemain += 1;
            }
            else
            {
                _mapManager.CreatePassage(new Coordinate(x, y), new Coordinate(newX, newY));
                x = newX;
                y = newY;
            }
            int nextDirection = pseudoRandom.Next(0, 4);
            while (direction % 2 == nextDirection % 2)
            {
                nextDirection = pseudoRandom.Next(0, 3);
            }
            direction = nextDirection;
        }
    }
}

