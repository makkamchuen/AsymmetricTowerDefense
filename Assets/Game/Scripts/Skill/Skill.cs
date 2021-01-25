using UnityEngine;

public class Skill: ActorActionComponent
{
  public SkillData skillData;
  [HideInInspector] public float cooldown;


  protected override void Start()
  {
    base.Start();
  }

  private void Update()
  {
    if (cooldown != 0)
    {
      cooldown -= Time.deltaTime;
      if (cooldown < 0)
      {
        cooldown = 0;
      }
    }
  }

  public void Cast(Vector3 destination)
  {
    actionScheduler.StartAction(this);
    skillData.Cast(actor, destination);
    cooldown = skillData.cooldown;
  }
  
  public bool CanHit(Actor target)
  {
    animator.SetTrigger("HeroAttack");
    return cooldown == 0 && skillData.InRange(actor, target);
  }

  public override void Cancel()
  {
    
  }
  
  // void OnDrawGizmos()
  // { 
  //   Gizmos.color = Color.red;
  //   Gizmos.DrawWireCube( actor.transform.position + new Vector3(actor.GetComponent<SpriteRenderer>().flipX? 0.25f * -1 : 0.25f, 0, 0), 
  //     new Vector3(0.5f, actor.transform.localScale.y, 0.5f));
  // }
}
