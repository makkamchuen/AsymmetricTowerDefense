using System;
using Cinemachine;
using UnityEngine;

public abstract class AttackSkillData : SkillData
{
  [SerializeField] private float damage;
  [TagField, SerializeField] private string[] _targetTags;
  
  protected float GetDamage()
  {
    return damage;
  }

  protected string[] GetTargetTags()
  {
    return _targetTags;
  }
}