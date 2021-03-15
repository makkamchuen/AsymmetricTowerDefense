using System;
using UnityEngine;

[Serializable]
public class SkillDataAtLevel
{
    [SerializeField] private int level;
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;

    public int Level => this.level;
    public float Damage => this.damage;
    public float Cooldown => this.cooldown;

    public SkillDataAtLevel(int level, float damage, float cooldown)
    {
        this.level = level;
        this.damage = damage;
        this.cooldown = cooldown;
    }
}