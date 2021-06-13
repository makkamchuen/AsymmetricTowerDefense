using System;
using Game.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : ActorActionComponent
{
  [SerializeField] GameObject hitEffect = null;
  [SerializeField] private GameObject nextLevelCharacter;
  [SerializeField] private GameObject rewardAfterDeath;
  public bool healthBarFollowCharacter = true;
  public bool hideHealBarWhenEmpty = true;
  public bool hideHealBarWhenFull = true;
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
    _currentHealth = GetActor().GetBaseStats().MaxHealth;
    SetMaxHealth();
    UpdateHealthBar();
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
    _currentHealth += GetActor().GetBaseStats().HealthRegenPerSecond;
    _currentHealth = Math.Min(_currentHealth, GetActor().GetBaseStats().MaxHealth);
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
      Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 0.5f), transform.rotation);
      FMODUnity.RuntimeManager.PlayOneShotAttached(hitSound, gameObject);
    }
    GetAnimator().SetTrigger(AnimationTrigger.hurt);
    if (!_isDead && _currentHealth <= 0)
    {
      GetAnimator().SetBool(AnimationTrigger.dead, true);
      GetActor().GetCollider().enabled = false;
      GetActor().GetStatus().SetGameFinishStatus();
      SpawnNextLevelCharacter();
      SpawnReward();
      _isDead = true;
      if (GetActor().tag.Equals("Enemy"))
      {
        Statistic.IncrementEnemyKilled();
      }
    }
  }

  private void SpawnNextLevelCharacter()
  {
    if (nextLevelCharacter != null) Instantiate(nextLevelCharacter, this.transform.position, Quaternion.identity);
  }

  private void SpawnReward()
  {
    var baseStats = gameObject.GetComponent<Actor>().GetBaseStats();
    if (baseStats.MinRewardFromCorpse <= baseStats.MaxRewardFromCorpse)
    {
      var rewardAmount = Random.Range(baseStats.MinRewardFromCorpse, baseStats.MaxRewardFromCorpse);
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
    if (hideHealBarWhenEmpty && _currentHealth == 0)
      healthBar.gameObject.SetActive(false);
    else if (hideHealBarWhenFull && _currentHealth == GetActor().GetBaseStats().MaxHealth)
      healthBar.gameObject.SetActive(false);
    else
      healthBar.gameObject.SetActive(true);
  }

  private void SetMaxHealth()
  {
    if (healthBar)
    {
      healthBar.SetMaxHealth(GetActor().GetBaseStats().MaxHealth);
    }
  }
}