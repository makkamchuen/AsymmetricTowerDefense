using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  [SerializeField] private Stats baseStats;
  [SerializeField] private Skill attack;
  [SerializeField] private Health _health;
  private Status _status;

  protected virtual void Start()
  {
    _status = new Status();
    _health = GetComponent<Health>();
  }

  public Skill GetAttackSkill()
  {
    return attack;
  }

  public Status GetStatus()
  {
    return _status;
  }
  
  public Stats GetBaseStats()
  {
    return baseStats;
  }
  
  public Health GetHealth()
  {
    return _health;
  }
}