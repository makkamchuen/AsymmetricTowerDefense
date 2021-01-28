using UnityEngine;

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
    if (_cooldown != 0)
    {
      return;
    }
    GetActionScheduler().StartAction(this);
    GetAnimator().SetTrigger(AnimationTrigger.attack);
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
    GetAnimator().SetBool(AnimationTrigger.attack, false);
  }

  void OnDrawGizmos()
  { 
    Gizmos.color = Color.red;
    Gizmos.DrawWireCube( transform.position + new Vector3(GetComponent<SpriteRenderer>().flipX? 0.25f * -1 : 0.25f, 0, 0), 
      new Vector3(0.5f, transform.localScale.y, 0.5f));
  }
}
