using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  [SerializeField] private Stats baseStats;
  [SerializeField] private Skill attack;
  private float _currentHealth;
  private Status _status = new Status();

  protected virtual void Start()
  {
    _currentHealth = baseStats.maxHealth;
  }

  public void Hit(float damage)
  {
    
  }

  public float GetCurrentHealth()
  {
    return _currentHealth;
  }

  public Skill GetAttackSkill()
  {
    return attack;
  }
}