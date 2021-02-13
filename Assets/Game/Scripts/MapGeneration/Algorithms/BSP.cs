using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BSP : MapGenerator
{
    [SerializeField] private int recursiveCount;
    [SerializeField] private bool verticalFirst;
    private bool drawPassage = true; // not implemented

    protected override void Start()
    {
        base.Start();
    }
    
    public override void GenerateMap(int width, int height, Random pseudoRandom)
    {
        _mapManager.map = new int[width,height];
        if (drawPassage)
        {
            _mapManager.FillMap(1);
        }
        Partition(1, width - 2, 1, height - 2, verticalFirst, recursiveCount, pseudoRandom);
    }

    private void Partition(int left, int right, int top, int bottom, bool splitVertical, int splitRemaining, System.Random pseudoRandom)
    {
        if (splitRemaining <= 0 || left >= right || top >= bottom)
        {
            return;
        }
        splitRemaining -= 1;
        if (splitVertical)
        {
            int target = pseudoRandom.Next(left, right + 1);
            _mapManager.CreatePassage(new Coordinate(target, top) , new Coordinate(target, bottom), drawPassage? 0: 1);
            Partition(left, target - 1, top, bottom, false, splitRemaining, pseudoRandom);
            Partition(target + 1, right, top, bottom, false, splitRemaining, pseudoRandom);
        }
        else
        {
            int target = pseudoRandom.Next(top, bottom + 1);
            _mapManager.CreatePassage(new Coordinate(left, target) , new Coordinate(right, target), drawPassage? 0: 1);
            Partition(left, right, top, target - 1, true, splitRemaining, pseudoRandom);
            Partition(left, right, target + 1, bottom, true, splitRemaining, pseudoRandom);
        }
    }
}
