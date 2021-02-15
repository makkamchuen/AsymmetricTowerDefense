using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "StateMachine/Actions/Flee")]
public class FleeAction : Action 
{
  public override void Act (AI ai)
  {
    if (ai is Minion minion)
    {
      Flee(minion);
    }
  }

  private void Flee(Minion minion)
  {
    var pos = RandomPoint(minion.transform.position, 2f);
    minion.GetMover().GetNavMeshAgent().SetDestination(pos);
  }
  
  Vector3 RandomPoint(Vector3 center, float range)
  {
    for (int i = 0; i < 30; i++)
    {
      Vector3 randomPoint = center + Random.insideUnitSphere * range;
      NavMeshHit hit;
      if (NavMesh.SamplePosition(randomPoint, out hit, 3f, NavMesh.AllAreas))
      {
        return hit.position;
      }
    }
    return center;
  }
}