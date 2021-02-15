using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu (menuName = "StateMachine/State/Flee State")]
public class FleeState: State
{
    public override void OnEnter(StateController controller)
    {
        base.OnEnter(controller);
        if (controller.target is Minion minion)
        {
            var pos = RandomPoint(minion.transform.position, 2f);
            minion.GetMover().GetNavMeshAgent().SetDestination(pos);
        }
    }

    public override void OnExit(StateController controller)
    {
        base.OnExit(controller);
        if (controller.target is Minion minion)
        {
            if (minion.GetTargetActor() == minion)
                return;
            Vector3 dir = minion.transform.position - minion.GetTargetActor().transform.position;
            dir.y = 0;//This allows the object to only rotate on its y axis
            Quaternion rot = Quaternion.LookRotation(dir);
            minion.transform.rotation = Quaternion.Lerp(minion.transform.rotation, rot, 0.01f);
        }
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