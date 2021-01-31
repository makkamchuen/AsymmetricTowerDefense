using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Mover _mover;
    private Skill[] _skills;
    private int _numOfTreasureCollected;
    
    protected override void Start()
    {
        base.Start();
        _mover = GetComponent<Mover>();
    }

    public Mover GetMover()
    {
        return _mover;
    }
    
    public void IncrementTreasureCollected()
    {
        _numOfTreasureCollected += 1;
    }

    public int GetNumOfTreasureCollected()
    {
        return _numOfTreasureCollected;
    }
}