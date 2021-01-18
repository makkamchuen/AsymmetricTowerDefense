using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action 
{
  public override void Act (StateController controller)
  {
    Patrol (controller);
  }

  private void Patrol(StateController controller)
  {
    if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance &&
        !controller.navMeshAgent.pathPending)
    {
      controller.stats.patrolPattern.Move(controller);
    }
  }
}