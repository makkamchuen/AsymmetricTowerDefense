using System;
using UnityEngine;

[Serializable]
public class SummonSkillDataAtLevel
{
    [SerializeField] private int level;
    [SerializeField] private float cooldown;
    [SerializeField] private int maxNumOfCast;

    public int Level => level;
    public float Cooldown => cooldown;
    public int MaxNumOfCast => maxNumOfCast;

    public SummonSkillDataAtLevel(int level, int maxNumOfCast, float cooldown)
    {
        this.level = level;
        this.maxNumOfCast = maxNumOfCast;
        this.cooldown = cooldown;
    }
}