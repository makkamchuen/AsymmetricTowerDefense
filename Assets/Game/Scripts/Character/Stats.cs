using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Stats"), Serializable]
public class Stats : ScriptableObject
{
  public float maxHealth;
  public float healthRegenPerSecond;
  public float movementSpeed;
  public float attackDamage;
  public int minRewardFromCorpse;
  public int maxRewardFromCorpse;
}
