using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Game.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Character/Stats"), Serializable]
public class Stats : ScriptableObject
{
  [SerializeField] private StatsAtLevel[] statsAtLevels;

  public float MaxHealth => this.CurrentStats.MaxHealth;
  public float HealthRegenPerSecond => this.CurrentStats.HealthRegenPerSecond;
  public float MovementSpeed => this.CurrentStats.MovementSpeed;
  public float AttackDamage => this.CurrentStats.AttackDamage;
  public int MinRewardFromCorpse => this.CurrentStats.MinRewardFromCorpse;
  public int MaxRewardFromCorpse => this.CurrentStats.MaxRewardFromCorpse;
  private SortedDictionary<int, StatsAtLevel> statsLevelDictionary;
  public FlyOrGround FlyOrGround = FlyOrGround.Ground;

  public StatsAtLevel CurrentStats
  {
    get
    {
      if (statsLevelDictionary == null)
      {
        StatsAtLevel prevLevelData = statsAtLevels[0];
        StatsAtLevel nextLevelData = statsAtLevels[statsAtLevels.Length > 1 ? 1 : 0];
        int nextLevelToAdvance = nextLevelData.Level + 1;
        int nextArrayIndex = 1;
        this.statsLevelDictionary = new SortedDictionary<int, StatsAtLevel>();
        for (int i = 1; i <= 100; i++)
        {
          if (i == nextLevelToAdvance)
          {
            prevLevelData = nextLevelData;
            nextLevelData = statsAtLevels[++nextArrayIndex];
            nextLevelToAdvance = nextLevelData.Level + 1;
          }

          float multiplier = Convert.ToSingle(i - prevLevelData.Level) / (nextLevelData.Level - prevLevelData.Level);
          if (multiplier is Single.NaN) multiplier = 0;
          
          float maxHealth = prevLevelData.MaxHealth + (nextLevelData.MaxHealth - prevLevelData.MaxHealth) * multiplier;
          float healthRegenPerSecond = prevLevelData.HealthRegenPerSecond + (nextLevelData.HealthRegenPerSecond - prevLevelData.HealthRegenPerSecond) * multiplier;
          float movementSpeed = prevLevelData.MovementSpeed + (nextLevelData.MovementSpeed - prevLevelData.MovementSpeed) * multiplier;
          float attackDamage = prevLevelData.AttackDamage + (nextLevelData.AttackDamage - prevLevelData.AttackDamage) * multiplier;
          int minRewardFromCorpse = prevLevelData.MinRewardFromCorpse + (nextLevelData.MinRewardFromCorpse - prevLevelData.MinRewardFromCorpse) * Convert.ToInt32(multiplier);
          int maxRewardFromCorpse = prevLevelData.MaxRewardFromCorpse + (nextLevelData.MaxRewardFromCorpse - prevLevelData.MaxRewardFromCorpse) * Convert.ToInt32(multiplier);
          StatsAtLevel data = new StatsAtLevel(i, maxHealth, healthRegenPerSecond, movementSpeed, attackDamage, minRewardFromCorpse, maxRewardFromCorpse);
          statsLevelDictionary.Add(i, data);
        }
      }

      return this.statsLevelDictionary[GameManager.CurrentLevel];
    }
  }
}

[Serializable]
public class StatsAtLevel
{
  [SerializeField] private int level;
  [SerializeField] private float maxHealth;
  [SerializeField] private float healthRegenPerSecond;
  [SerializeField] private float movementSpeed;
  [SerializeField] private float attackDamage;
  [SerializeField] private int minRewardFromCorpse;
  [SerializeField] private int maxRewardFromCorpse;

  public StatsAtLevel(int level, float maxHealth, float healthRegenPerSecond, float movementSpeed, float attackDamage,
    int minRewardFromCorpse, int maxRewardFromCorpse)
  {
    this.level = level;
    this.maxHealth = maxHealth;
    this.healthRegenPerSecond = healthRegenPerSecond;
    this.movementSpeed = movementSpeed;
    this.attackDamage = attackDamage;
    this.minRewardFromCorpse = minRewardFromCorpse;
    this.maxRewardFromCorpse = maxRewardFromCorpse;
  }

  public int Level => level;
  public float MaxHealth => maxHealth;

  public float HealthRegenPerSecond => healthRegenPerSecond;

  public float MovementSpeed => movementSpeed;

  public float AttackDamage => attackDamage;
  public int MinRewardFromCorpse => minRewardFromCorpse;
  public int MaxRewardFromCorpse => maxRewardFromCorpse;
}
