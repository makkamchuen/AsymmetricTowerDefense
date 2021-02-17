using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[CreateAssetMenu (menuName = "StateMachine/State/Flee State")]
public class FleeState: State
{
    private Actor _target;
    private Vector3 _prevPosition;
    
    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        _target = controller.target.GetTargetActor();
        _prevPosition = controller.transform.position;
        if (controller.target is Minion minion)
        {
            var pos = RandomPoint(minion.transform.position, 1.5f, 4f);
            minion.GetMover().MoveTo(pos);
        }
    }

    public override void OnExit(StateController controller)
    {
        base.OnExit(controller);
        if (controller.target is Minion minion)
        {
            minion.GetMover().MoveTo(_prevPosition);
        }
    }

    Vector3 RandomPoint(Vector3 center, float minDist, float maxDist)
    {
        for (int i = 0; i < 300; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * Random.Range(minDist, maxDist);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 0.01f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }
}