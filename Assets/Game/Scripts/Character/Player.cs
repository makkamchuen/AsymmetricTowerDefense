using System.Collections;
using System.Collections.Generic;
using Game.Scripts;
using UnityEngine;

public class Player : Actor
{
    private Mover _mover;
    private Skill[] _skills;
    private int _rewardAmountCollected;
    private int _rewardAmountMax;
    
    
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

    public int RewardAmountMax
    {
        get => _rewardAmountMax;
        set => _rewardAmountMax = value;
    }

    public void IncrementRewardCollected(int amount)
    {
        _rewardAmountCollected += amount;
        _rewardAmountCollected = _rewardAmountCollected > RewardAmountMax
            ? RewardAmountMax
            : _rewardAmountCollected;
    }
    
    public void DecrementRewardCollected(int amount)
    {
        _rewardAmountCollected -= amount;
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