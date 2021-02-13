using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LWRP;
using Random = System.Random;

public class ArticulationPoint : MapGenerator
{
    [SerializeField] private int startRow;
    [SerializeField] private int startCol;
    [SerializeField] private int endCol;
    [SerializeField] private int endRow;
    [SerializeField] private bool useRandomEndRow;

    protected override void Start()
    {
        base.Start();
    }

    public override void GenerateMap(int width, int height, Random pseudoRandom)
    {
        int pathWidth = 1 + _mapManager.pathRadius * 2;
        _mapManager.map = new int[width,height];
        _mapManager.FillMap(1);
        height /= pathWidth;
        width /= pathWidth;
        if (useRandomEndRow)
        {
            endRow = -1;
        }
        Dictionary<Coordinate, Coordinate> dfsPath = new Dictionary<Coordinate, Coordinate>();
        if (startRow < 0 || startRow >= height || 
            endRow < -1 || endRow >= height || 
            startCol < 0 || startCol >= width || 
            endCol < 0 || endCol >= width || 
            endCol < startCol ||
            (endCol - startCol) % 2 != 0)
        {
            return;
        }
        Coordinate startCoordinate = new Coordinate(startCol, startRow);
        dfsPath[startCoordinate] = new Coordinate(startCol - 1, startRow);
        Dfs(startCoordinate, pseudoRandom, ref dfsPath, endRow, height);
        if (endRow == -1)
        {
            int maxLength = -1;
            for (int i = startRow % 2; i < height; i += 2)
            {
                int length = GetPathLength(new Coordinate(endCol, i), dfsPath);
                if (length > maxLength)
                {
                    maxLength = length;
                    endRow = i;
                }
            }
        }
        Backtrack(new Coordinate(endCol, endRow), dfsPath);
        _mapManager.CreatePassage(ScaledCoordinateToOriginal(new Coordinate(-1, startRow)), ScaledCoordinateToOriginal(new Coordinate(startCol, startRow)));
        _mapManager.CreatePassage(ScaledCoordinateToOriginal(new Coordinate(width, endRow)), ScaledCoordinateToOriginal(new Coordinate(endCol, endRow)));
    }
    
    private int GetPathLength(Coordinate current, Dictionary<Coordinate, Coordinate> dfsPath)
    {
        if (!dfsPath.ContainsKey(current))
        {
            return 0;
        }
        return 1 + GetPathLength(dfsPath[current], dfsPath);
    }

    private void Backtrack(Coordinate current, Dictionary<Coordinate, Coordinate> dfsPath)
    {
        if (!dfsPath.ContainsKey(current))
        {
            return;
        }
        _mapManager.CreatePassage(ScaledCoordinateToOriginal(current), ScaledCoordinateToOriginal(dfsPath[current]));
        Backtrack(dfsPath[current], dfsPath);
    }

    private Coordinate ScaledCoordinateToOriginal(Coordinate coordinate)
    {
        return new Coordinate((_mapManager.pathRadius * 2 + 1) * coordinate.tileX + _mapManager.pathRadius,
            (_mapManager.pathRadius * 2 + 1) * coordinate.tileY + _mapManager.pathRadius);
    }

    private void Dfs(Coordinate current, Random pseudoRandom, ref Dictionary<Coordinate, Coordinate> dfsPath, int endRow, int height)
    {
        if (current.tileX == endCol && current.tileY == endRow)
        {
            return;
        }
        Tuple<int, int>[] directions = {new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 0), new Tuple<int, int>(0, -1), new Tuple<int, int>(-1, 0)};
        HashSet<int> indexSet = new HashSet<int>();
        while (indexSet.Count < 4)
        {
            int randomIndex = pseudoRandom.Next(0, directions.Length);
            while (indexSet.Contains(randomIndex))
            {
                randomIndex = pseudoRandom.Next(0, directions.Length);
            }
            indexSet.Add(randomIndex);
            Tuple<int, int> direction = directions[randomIndex];
            Coordinate next = new Coordinate(current.tileX + direction.Item1 * 2, current.tileY + direction.Item2 * 2);
            if (next.tileX < startCol || next.tileX > endCol || 
                next.tileY < 0 || next.tileY >= height || 
                dfsPath.ContainsKey(next))
            {
                continue;
            }
            dfsPath[next] = current;
            Dfs(next, pseudoRandom, ref dfsPath, endRow, height);
        }
    }
}
