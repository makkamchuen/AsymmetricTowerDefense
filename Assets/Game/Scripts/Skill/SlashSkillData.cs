
using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Attack/Slash")]
public class SlashSkillData : SkillData
{
  public AttackInfo attackInfo;
  public float hitBoxHeight;
  public float hitBoxWidth;

  public override void Cast(Actor user, Vector3 destination)
  {
    float xOffset = hitBoxWidth / 2;
    Collider[] colliders = Physics.OverlapBox(
      user.transform.position + new Vector3(user.GetComponentInChildren<SpriteRenderer>().flipX? xOffset * -1 : xOffset, 0, 0), 
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
    foreach (Collider collider in colliders)
    {
      if (!attackInfo.IsTarget(collider.tag))
      {
        continue;
      }
      collider.GetComponent<Health>().Hit(attackInfo.GetDamage() + user.GetBaseStats().attackDamage);
    }
  }

  public override bool InRange(Actor user, Actor targetActor)
  {
    float xOffset = hitBoxWidth / 2;
    Collider[] colliders = Physics.OverlapBox(
      user.transform.position + new Vector3(user.GetSpriteRender().flipX? xOffset * -1 : xOffset, 0, 0), 
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
    return colliders.Contains(targetActor.GetComponent<Collider>());
  }
}