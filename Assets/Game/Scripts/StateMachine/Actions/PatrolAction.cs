using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Actions/Patrol")]
public class PatrolAction : Action 
{
  public override void Act (AI ai)
  {
    if (ai is Minion minion)
    {
      Patrol(minion);
    }
  }

  private void Patrol(Minion minion)
  {
    if (minion.GetMover().GetNavMeshAgent().remainingDistance <= minion.GetMover().GetNavMeshAgent().stoppingDistance
        && !minion.GetMover().GetNavMeshAgent().pathPending)
    {
      minion.GetPatrolPattern().Move(minion.GetMover());
    }
  }
}