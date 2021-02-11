using UnityEngine;
using UnityEngine.AI;

public class Skill: ActorActionComponent
{
  [SerializeField] private SkillData[] skillDatas;
  private float _cooldown;
  private float _channelTime;
  private Vector3 _destination;
  private SkillData skillDataToUse;
  [SerializeField] private SkillData skillData;
  
  protected override void Start()
  {
    base.Start();
    skillDataToUse = skillDatas[0];
  }

  private void Update()
  {
    if (!skillDataToUse) return;
    if (_cooldown != 0)
    {
      _cooldown -= Time.deltaTime;
      if (_cooldown <= 0)
      {
        _cooldown = 0;
      }
    }

    if (_channelTime != 0)
    {
      _channelTime -= Time.deltaTime;
      if (_channelTime <= 0)
      {
        skillDataToUse.Cast(GetActor(), _destination);
        _channelTime = 0;
      }
    }
  }

  public void Cast(Vector3 destination)
  {
    if (!skillDataToUse || _cooldown != 0 || !GetActor().GetStatus().Attackable())
    {
      return;
    }
    GetAnimator().SetTrigger(AnimationTrigger.attack);
    GetActor().SetIsFacingRight(destination.x > transform.position.x);
    GetActionScheduler().StartAction(this);
    _cooldown = skillDataToUse.GetCoolDown();
    _channelTime = skillDataToUse.GetChannelTime();
    _destination = destination;
  }

  public bool CanHit(Actor target)
  {
    if (CanHitTarget(target)) return true;
    foreach (var skillData in skillDatas)
    {
      skillDataToUse = skillData;
      if (CanHitTarget(target)) return true;
    }

    skillDataToUse = null;
    return false;
  }

  public bool OnCoolDown()
  {
    return _cooldown != 0;
  }

  public override void Cancel()
  {
    _channelTime = 0;
    GetAnimator().SetBool(AnimationTrigger.attack, false);
  }

  void OnDrawGizmos()
  { 
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube( transform.position + new Vector3(GetComponentInChildren<SpriteRenderer>().flipX? 0.25f * -1 : 0.25f, 0, 0), 
      new Vector3(0.5f, transform.localScale.y, 0.5f));
  }


  private bool CanHitTarget(Actor target)
  {
    return skillDataToUse && skillDataToUse.CanApply(GetActor(), target);
  }

}

