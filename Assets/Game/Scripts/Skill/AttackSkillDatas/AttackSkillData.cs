using Cinemachine;

public abstract class AttackSkillData : SkillData
{
  private float damage;
  [TagField] private string[] _targetTags;
  
  protected float GetDamage()
  {
    return damage;
  }

  protected string[] GetTargetTags()
  {
    return _targetTags;
  }
}