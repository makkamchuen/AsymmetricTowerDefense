using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Game.Scripts;
using UnityEngine;

public abstract class AttackSkillData : SkillData
{
  [SerializeField] protected SkillDataAtLevel[] skillDataAtLevel;
  [TagField, SerializeField] protected string[] _targetTags;
  [SerializeField] protected GameObject hitEffect = null;
  [SerializeField][FMODUnity.EventRef] protected string hitSound;

  private SortedDictionary<int, SkillDataAtLevel> skillDataAtLevelDictionary;
  
  public SkillDataAtLevel CurrentSkillData
  {
    get
    {
      if (skillDataAtLevelDictionary == null)
      {
        SkillDataAtLevel prevLevelData = this.skillDataAtLevel[0];
        SkillDataAtLevel nextLevelData = this.skillDataAtLevel[this.skillDataAtLevel.Length > 1 ? 1 : 0];
        int nextLevelToAdvance = nextLevelData.Level + 1;
        int nextArrayIndex = 1;
        this.skillDataAtLevelDictionary = new SortedDictionary<int, SkillDataAtLevel>();
        for (int i = 1; i <= 100; i++)
        {
          if (i == nextLevelToAdvance)
          {
            prevLevelData = nextLevelData;
            nextLevelData = skillDataAtLevel[++nextArrayIndex];
            nextLevelToAdvance = nextLevelData.Level + 1;
          }

          float multiplier = Convert.ToSingle(i - prevLevelData.Level) / (nextLevelData.Level - prevLevelData.Level);
          if (multiplier is Single.NaN || multiplier is Single.PositiveInfinity) multiplier = 0;

          float damage = prevLevelData.Damage + (nextLevelData.Damage - prevLevelData.Damage) * multiplier;
          float cooldown = prevLevelData.Cooldown + (nextLevelData.Cooldown - prevLevelData.Cooldown) * multiplier;
          SkillDataAtLevel data = new SkillDataAtLevel(i, damage, cooldown);
          this.skillDataAtLevelDictionary.Add(i, data);
        }
      }

      return this.skillDataAtLevelDictionary[GameManager.currentLevel];
    }
  }
  
  public float GetDamage()
  {
    return this.CurrentSkillData.Damage;
  }
  
  public override float GetCooldown()
  {
    return this.CurrentSkillData.Cooldown;
  }

  public bool IsTarget(string tag)
  {
    return Array.IndexOf(_targetTags, tag) > -1;
  }

  public HashSet<string> GetTargetSet()
  {
    HashSet<string> tagSet = new HashSet<string>();
    foreach (string tag in _targetTags)
    {
      tagSet.Add(tag);
    }

    return tagSet;
  }



}