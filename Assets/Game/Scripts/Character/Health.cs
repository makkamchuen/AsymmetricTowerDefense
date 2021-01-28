using UnityEngine;

public class Health : ActorActionComponent
{
  private float _currentHealth;

  protected override void Start()
  {
    base.Start();
    _currentHealth = GetActor().GetBaseStats().maxHealth;
  }

  private void Update()
  {
    
  }

  public void Hit(float damage)
  {
    GetActionScheduler().StartAction(this);
    _currentHealth -= damage;
    GetAnimator().SetTrigger(AnimationTrigger.hurt);
    if (_currentHealth <= 0)
    {
      GetAnimator().SetBool(AnimationTrigger.dead, true);
    }
  }

  public float GetCurrentHealth()
  {
    return _currentHealth;
  }
  
  public override void Cancel()
  {
    GetAnimator().SetBool(AnimationTrigger.hurt, false);
  }
}