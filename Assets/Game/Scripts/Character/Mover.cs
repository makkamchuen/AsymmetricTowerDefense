using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent
{
    private NavMeshAgent navMeshAgent;
    
    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetAnimator().SetBool(AnimationTrigger.run, false);
    }

    private void Update()
    {
        UpdateAction();
        navMeshAgent.enabled = GetActor().GetHealth().GetCurrentHealth() > 0;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }

    public void MoveTo(Vector3 destination)
    {
        if (!GetActor().GetStatus().Moveable())
        {
            return;
        }

        if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            return;
        }
        GetAnimator().SetBool(AnimationTrigger.run, true);
        GetActor().SetIsFacingRight(hit.position.x > transform.position.x);
        navMeshAgent.destination = hit.position;
        navMeshAgent.isStopped = false;
        GetActionScheduler().StartAction(this);
    }

    private void UpdateAction()
    {
        if (GetActionScheduler().GetCurrentAction() is Mover mover && 
            mover == this &&
            !navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            GetActionScheduler().CancelCurrentAction();
        } 
    }
    
    public override void Cancel()
    {
        GetAnimator().SetBool(AnimationTrigger.run, false);
        navMeshAgent.destination = transform.position;
        navMeshAgent.isStopped = true;
    }
}