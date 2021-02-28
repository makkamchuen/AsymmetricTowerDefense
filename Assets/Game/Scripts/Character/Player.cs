using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private Mover _mover;
    private Skill[] _skills;
    private int _rewardAmountCollected;
    
    protected override void Start()
    {
        base.Start();
        _mover = GetComponent<Mover>();
        _rewardAmountCollected = 0;
    }

    public Mover GetMover()
    {
        return _mover;
    }
    
    public void IncrementRewardCollected(int amount)
    {
        _rewardAmountCollected += amount;
    }
    
    public void ResetRewardAmountCollected()
    {
        _rewardAmountCollected = 0;
    }

    public int GetRewardAmountCollected()
    {
        return _rewardAmountCollected;
    }
}