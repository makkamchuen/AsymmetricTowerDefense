using UnityEngine;
using UnityEngine.AI;

public class Skill: ActorActionComponent
{
  [SerializeField] private SkillData skillData;
  private float _cooldown;
  private float _channelTime;
  private Vector3 _destination;

  protected override void Start()
  {
    base.Start();
  }

  private void Update()
  {
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
        skillData.Cast(GetActor(), _destination);
        _channelTime = 0;
      }
    }
  }

  public void Cast(Vector3 destination)
  {
    if (_cooldown != 0 || !GetActor().GetStatus().Attackable())
    {
      return;
    }
    GetAnimator().SetTrigger(AnimationTrigger.attack);
    GetActor().SetIsFacingRight(destination.x > transform.position.x);
    GetActionScheduler().StartAction(this);
    _cooldown = skillData.GetCoolDown();
    _channelTime = skillData.GetChannelTime();
    _destination = destination;
  }

  public bool CanHit(Actor target)
  {
    return skillData.InRange(GetActor(), target);
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
}
