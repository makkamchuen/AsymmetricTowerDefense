using System;
using Game.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : ActorActionComponent
{
  [SerializeField] GameObject hitEffect = null;
  [SerializeField] private GameObject rewardAfterDeath;
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
      SpawnReward();
      _isDead = true;
    }
  }

  private void SpawnReward()
  {
    var baseStats = gameObject.GetComponent<Actor>().GetBaseStats();
    if (baseStats.minRewardFromCorpse <= baseStats.maxRewardFromCorpse)
    {
      var rewardAmount = Random.Range(baseStats.minRewardFromCorpse, baseStats.maxRewardFromCorpse);
      if (rewardAmount > 0)
      {
        GameObject reward = Instantiate(rewardAfterDeath, this.transform.position, Quaternion.identity);
        reward.GetComponent<Reward>().Amount = rewardAmount;
      }
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