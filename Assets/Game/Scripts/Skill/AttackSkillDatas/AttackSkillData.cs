using Cinemachine;

public abstract class AttackSkillData : SkillData
{
  public float damage;
  [TagField] public string[] targetTags;
}