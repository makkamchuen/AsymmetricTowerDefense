using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "StateMachine/Actions/Face to Target")]
public class FaceToTargetAction : Action 
{
  public override void Act (AI ai)
  {
    if (ai is Minion minion)
    {
      FaceTarget(minion);
    }
  }

  private void FaceTarget(Minion minion)
  {
    if (minion.GetTargetActor() == minion)
      return;
    Vector3 dir = minion.transform.position - minion.GetTargetActor().transform.position;
    dir.y = 0;//This allows the object to only rotate on its y axis
    Quaternion rot = Quaternion.LookRotation(dir);
    minion.transform.rotation = Quaternion.Lerp(minion.transform.rotation, rot, 0.01f);
  }
  
}