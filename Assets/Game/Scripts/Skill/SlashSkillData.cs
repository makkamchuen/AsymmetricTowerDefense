using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Game.Scripts;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Slash")]
public class SlashSkillData : AttackSkillData
{

  public float hitBoxHeight;
  public float hitBoxWidth;
  
  public override void Cast(Actor user)
  {
    float xOffset = hitBoxWidth / 2;
    Collider[] colliders = Physics.OverlapBox(
      user.transform.position + new Vector3(user.GetSpriteRender().flipX? xOffset * -1 : xOffset, 0, 0),
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
    foreach (Collider collider in colliders)
    {
      if (!IsTarget(collider.tag) || !CanTakeDownTarget(collider.GetComponent<Actor>()))
      {
        continue;
      }
      collider.GetComponent<Health>().Hit(GetDamage() + user.GetBaseStats().AttackDamage, hitEffect, hitSound);
    }
  }

  public override bool CanApply(Actor user, Actor targetActor)
  {
    if (!CanTakeDownTarget(targetActor) || !IsTarget(targetActor.tag)) return false;

    float xOffset = hitBoxWidth / 2;
    Collider[] colliders = Physics.OverlapBox(
      user.transform.position + new Vector3(user.GetSpriteRender().flipX? xOffset * -1 : xOffset, 0, 0),
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
    return colliders.Contains(targetActor.GetCollider());
  }
}