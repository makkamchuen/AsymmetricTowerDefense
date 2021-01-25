using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[CreateAssetMenu (menuName = "AI/PatrolPattern/Generic")]
public class GenericPatrolPattern : PatrolPattern
{
  [SerializeField] private float minMoveDistance;
  [SerializeField] private float maxMoveDistance;
  [SerializeField] private float minIdleSec;
  [SerializeField] private float maxIdleSec;
  
  public override void Move(Mover mover)
  {
    Travel(mover);
  }

  private void Travel(Mover mover) // Idle not Implemented
  {
    float idleSec = Random.Range(minIdleSec, maxIdleSec); 
    NavMeshHit hit;
    if (NavMesh.SamplePosition(GenerateOffset() + mover.transform.position, out hit, maxMoveDistance, 1))
    {
      mover.MoveTo(hit.position);
    }
  }

  private Vector3 GenerateOffset()
  {
    Vector3 offset = Random.insideUnitSphere * (maxMoveDistance - minMoveDistance);
    if (offset.x != 0)
    {
      offset.x += minMoveDistance * (offset.x > 0 ? 1 : -1);
    }
    if (offset.y != 0)
    {
      offset.y += minMoveDistance * (offset.y > 0 ? 1 : -1);
    }
    if (offset.z != 0)
    {
      offset.z += minMoveDistance * (offset.z > 0 ? 1 : -1);
    }
    return offset;
  }
}