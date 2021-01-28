using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Sight"), Serializable]
public class Sight: ScriptableObject
{
  [SerializeField] private float radius;
  [SerializeField] private Priority priority;
  [TagField, SerializeField] private string[] targetTags;

  public bool SearchTarget(AI ai)
  {
    bool found = false;
    bool sameTag = false;
    Collider[] colliders = Physics.OverlapSphere(ai.transform.position, radius);
    foreach (Collider collider in colliders)
    {
      foreach (string tag in targetTags)
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
      Actor target = collider.GetComponentInChildren<Actor>();
      if (target.GetHealth().GetCurrentHealth() > 0 && (ai.GetTargetActor() == ai || priority.Compare(ai, ai.GetTargetActor(), target)))
      {
        ai.SetTargetActor(target);
        found = true;
      }
    }
    if (!found)
    {
      ai.SetTargetActor(ai);
    }
    return found;
  }
}