using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Decisions/ActiveState")]
public class ActiveStateDecision : Decision 
{
  public override bool Decide (StateController controller)
  {
    if (controller.ai.target == controller.ai)
    {
      return false;
    }
    return controller.ai.target.currentHealth > 0;
  }
}