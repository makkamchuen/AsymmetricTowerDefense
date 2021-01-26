using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Actions/Chase")]
public class ChaseAction : Action 
{
  public override void Act (AI ai)
  {
    if (ai is Minion minion)
    {
      Chase(minion);
    }
  }

  private void Chase(Minion minion)
  {
    minion.GetMover().MoveTo(minion.GetTargetActor().transform.position);
  }
}