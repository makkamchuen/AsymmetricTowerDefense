using UnityEngine;

public class Skill: ActorActionComponent
{
  [SerializeField] private SkillData skillData;
  private float _cooldown;
  
  protected override void Start()
  {
    base.Start();
  }

  private void Update()
  {
    if (_cooldown != 0)
    {
      _cooldown -= Time.deltaTime;
      if (_cooldown < 0)
      {
        _cooldown = 0;
      }
    }
  }

  public void Cast(Vector3 destination)
  {
    GetAnimator().SetTrigger(AnimationTrigger.attack);
    GetActionScheduler().StartAction(this);
    skillData.Cast(GetActor(), destination);
    _cooldown = skillData.GetCoolDown();
  }
  
  public bool CanHit(Actor target)
  {
    return _cooldown == 0;
  }

  public override void Cancel()
  {
    GetAnimator().SetBool(AnimationTrigger.attack, false);
  }

  // void OnDrawGizmos()
  // { 
  //   Gizmos.color = Color.red;
  //   Gizmos.DrawWireCube( transform.position + new Vector3(GetComponent<SpriteRenderer>().flipX? 0.25f * -1 : 0.25f, 0, 0), 
  //     new Vector3(0.5f, transform.localScale.y, 0.5f));
  // }
}
