using System;
using UnityEngine;

public class Health : ActorActionComponent
{
  private float _currentHealth;
  private bool _isDead;

  private void Awake()
  {
    InvokeRepeating(nameof(RegenerateHealth), 0.0f, 1.0f);
  }
  
  protected override void Start()
  {
    base.Start();
    _currentHealth = GetActor().GetBaseStats().maxHealth;
    _isDead = false;
  }

  private void Update()
  {
  }

  private void RegenerateHealth()
  {
    if (_isDead)
    {
      return;
    }
    _currentHealth += GetActor().GetBaseStats().healthRegenPerSecond;
    _currentHealth = Math.Min(_currentHealth, GetActor().GetBaseStats().maxHealth);
  }

  public void Hit(float damage)
  {
    GetActionScheduler().StartAction(this);
    _currentHealth -= damage;
    _currentHealth = Math.Max(_currentHealth, 0);
    GetAnimator().SetTrigger(AnimationTrigger.hurt);
    if (!_isDead && _currentHealth <= 0)
    {
      GetAnimator().SetBool(AnimationTrigger.dead, true);
      GetComponentInParent<Collider>().enabled = false;
      GetActor().GetStatus().SetDeadStatus();
      _isDead = true;
    }
  }

  public float GetCurrentHealth()
  {
    return _currentHealth;
  }

  public bool GetIsDead()
  {
    return _isDead;
  }
  
  public override void Cancel()
  {
    GetAnimator().SetBool(AnimationTrigger.hurt, false);
  }
}