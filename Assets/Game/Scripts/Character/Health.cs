using System;
using UnityEngine;

public class Health : ActorActionComponent
{
  [SerializeField] GameObject hitEffect = null;
  public bool healthBarFollowCharacter = true;
  public HealthBar healthBar;

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
    SetMaxHealth();
    _isDead = false;
  }

  private void Update()
  {
    if (healthBarFollowCharacter)
      healthBar.transform.position = Camera.main.WorldToScreenPoint(GetActor().transform.position);
  }

  private void RegenerateHealth()
  {
    if (_isDead)
    {
      return;
    }
    _currentHealth += GetActor().GetBaseStats().healthRegenPerSecond;
    _currentHealth = Math.Min(_currentHealth, GetActor().GetBaseStats().maxHealth);
    UpdateHealthBar();
  }

  public void Hit(float damage, GameObject hitEffect, string hitSound)
  {
    GetActionScheduler().StartAction(this);
    _currentHealth -= damage;
    _currentHealth = Math.Max(_currentHealth, 0);
    UpdateHealthBar();
    if (hitEffect != null)
    {
      Instantiate(hitEffect, transform.position, transform.rotation);
      FMODUnity.RuntimeManager.PlayOneShotAttached(hitSound, gameObject);
    }
    GetAnimator().SetTrigger(AnimationTrigger.hurt);
    if (!_isDead && _currentHealth <= 0)
    {
      GetAnimator().SetBool(AnimationTrigger.dead, true);
      GetActor().GetCollider().enabled = false;
      GetActor().GetStatus().SetGameFinishStatus();
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

  private void UpdateHealthBar()
  {
    if (healthBar)
    {
      healthBar.SetHealth(_currentHealth);
    }
  }

  private void SetMaxHealth()
  {
    if (healthBar)
    {
      healthBar.SetMaxHealth(GetActor().GetBaseStats().maxHealth);
    }
  }
}