using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Sight"), Serializable]
public class Sight: ScriptableObject
{
  [SerializeField] private float radius;
  [SerializeField] private Priority priority;

  public bool SearchTarget(AI ai)
  {
    var skillDatas = ai.Skill.SkillDatas;
    bool found = false;
    bool sameTag = false;
    Collider[] colliders = Physics.OverlapSphere(ai.transform.position, radius);
    foreach (Collider collider in colliders)
    {
      foreach (var skillData in skillDatas)
      {
        if (!skillData.IsTarget(collider.tag)) continue;
        
        Actor target = collider.GetComponent<Actor>();
        if (ai.GetTargetActor() == ai || priority.Compare(ai, ai.GetTargetActor(), target))
        {
          ai.SetTargetActor(target);
          found = true;
        }
      }
    }
    if (!found)
    {
      ai.SetTargetActor(ai);
    }
    return found;
  }
}