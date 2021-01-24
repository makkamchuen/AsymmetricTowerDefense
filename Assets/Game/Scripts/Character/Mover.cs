using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent, IAction
{

    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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

    public void Cancel()
    {
        navMeshAgent.isStopped = true;
    }
}
        
