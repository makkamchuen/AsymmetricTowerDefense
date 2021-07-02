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
    Collider[] colliders = GetCollidersInHitBox(user);
    foreach (Collider collider in colliders)
    {
      var actor = collider.GetComponent<Actor>();
      if (actor == null || !CanTakeDownTarget(actor))
      {
        continue;
      }
      collider.GetComponent<Health>().Hit(GetDamage() + user.GetBaseStats().AttackDamage, hitEffect, hitSound);
    }
  }

  public override bool CanApply(Actor user, Actor targetActor)
  {
    if (!CanTakeDownTarget(targetActor)) return false;

    Collider[] colliders = GetCollidersInHitBox(user);
    return colliders.Contains(targetActor.GetCollider());
  }

  private Collider[] GetCollidersInHitBox(Actor user)
  {
    return Physics.OverlapBox(
      user.transform.position + new Vector3( hitBoxWidth / 2 * (user.IsFacingRight == user.FaceRightByDefault? -1 : 1), 0, 0), 
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
  }
}