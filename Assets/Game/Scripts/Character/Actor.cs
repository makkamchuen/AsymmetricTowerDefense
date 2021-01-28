using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  [SerializeField] private Stats baseStats;
  [SerializeField] private Skill attack;
  [SerializeField] private Animator animator;
  [SerializeField] private ActionScheduler actionScheduler;
  private float _currentHealth;
  private Status _status;

  protected virtual void Start()
  {
    _currentHealth = baseStats.maxHealth;
    _status = new Status();
  }

  public void Hit(float damage)
  {
    actionScheduler.CancelCurrentAction();
    _currentHealth -= damage;
    animator.SetTrigger(AnimationTrigger.hurt);
    if (_currentHealth <= 0)
    {
      animator.SetBool(AnimationTrigger.dead, true);
    }
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