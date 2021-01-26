
using System.Linq;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Attack/Slash")]
public class SlashSkillData : AttackSkillData
{
  public float hitBoxHeight;
  public float hitBoxWidth;
  
  public override void Cast(Actor user, Vector3 destination)
  {
    float xOffset = hitBoxWidth / 2;
    bool sameTag = false;
    Collider[] colliders = Physics.OverlapBox(
      user.transform.position + new Vector3(user.GetComponent<SpriteRenderer>().flipX? xOffset * -1 : xOffset, 0, 0), 
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
    foreach (Collider collider in colliders)
    {
      foreach (string tag in GetTargetTags())
      {
        if (collider.CompareTag(tag))
        {
          sameTag = true;
          break;
        }
      }
      if (!sameTag)
      {
        continue;
      }
      sameTag = false;
      collider.GetComponent<Actor>().Hit(GetDamage());
    }
  }

  public override bool InRange(Actor user, Actor targetActor)
  {
    float xOffset = hitBoxWidth / 2;
    Collider[] colliders = Physics.OverlapBox(
      user.transform.position + new Vector3(user.GetComponent<SpriteRenderer>().flipX? xOffset * -1 : xOffset, 0, 0), 
      new Vector3(hitBoxWidth, user.transform.localScale.y, hitBoxHeight));
    return colliders.Contains(targetActor.GetComponent<Collider>());
  }
}