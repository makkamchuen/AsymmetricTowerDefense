using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  [SerializeField] private Stats baseStats;
  [SerializeField] private Skill attack;
  private float _currentHealth;
  private Status _status;

  protected virtual void Start()
  {
    _currentHealth = baseStats.maxHealth;
    _status = new Status();
  }

  public void Hit(float damage)
  {
    Debug.Log(this);
  }

  public float GetCurrentHealth()
  {
    return _currentHealth;
  }

  public Skill GetAttackSkill()
  {
    return attack;
  }

  public Status GetStatus()
  {
    return _status;
  }
}