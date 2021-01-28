using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Decisions/ActiveState")]
public class ActiveStateDecision : Decision 
{
  public override bool Decide (StateController controller)
  {
    if (controller.target.GetTargetActor() == controller.target)
    {
      return false;
    }
    return !controller.target.GetTargetActor().GetHealth().GetIsDead();
  }
}