using System;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Addon/Sight"), Serializable]
public class Sight: ScriptableObject
{
  [SerializeField] private float radius;
  [TagField, SerializeField] private string targetTag;

  public bool SearchTarget(StateController controller)
  {
    bool found = false;
    Collider[] colliders = Physics.OverlapSphere(controller.transform.position, radius);
    foreach (Collider collider in colliders) // may be move logic out to Decision?
    {
      if (collider.CompareTag(targetTag) 
          && Compare(collider.gameObject, controller.gameObject, controller)
          )
      {
        controller.target = collider.gameObject;
        found = true;
        break;
      }
    }
    return found;
  }

  private bool Compare(GameObject candidate, GameObject original, StateController controller)
  {
    return true;
  }
}