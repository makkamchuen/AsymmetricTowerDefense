using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent
{

    public NavMeshAgent navMeshAgent;

    protected override void Start()
    {
        base.Start();
        // navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.enabled = actor.currentHealth > 0;
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }

    public override void Cancel()
    {
        navMeshAgent.isStopped = true;
    }
}
        
