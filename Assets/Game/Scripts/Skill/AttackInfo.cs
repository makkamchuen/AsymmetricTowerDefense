using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[Serializable]
public class AttackInfo
{
  [SerializeField] private float damage;
  [TagField, SerializeField] private string[] _targetTags;

  public float GetDamage()
  {
    return damage;
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