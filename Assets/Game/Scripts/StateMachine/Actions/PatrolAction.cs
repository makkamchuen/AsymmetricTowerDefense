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
    if (minion.mover.navMeshAgent.remainingDistance <= minion.mover.navMeshAgent.stoppingDistance
        && !minion.mover.navMeshAgent.pathPending)
    {
      minion.patrolPattern.Move(minion.mover);
    }
  }
}