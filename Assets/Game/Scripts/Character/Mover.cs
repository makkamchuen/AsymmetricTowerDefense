using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent
{
    private NavMeshAgent navMeshAgent;
    private float prevPositionX;
    
    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = GetActor().GetBaseStats().MovementSpeed;
        GetAnimator().SetBool(AnimationTrigger.run, false);
        prevPositionX = transform.position.x;
    }

    private void Update()
    {
        UpdateAction();
        navMeshAgent.enabled = GetActor().GetHealth().GetCurrentHealth() > 0;
        if (transform.position.x != prevPositionX)
        {
            GetActor().SetIsFacingRight(transform.position.x > prevPositionX);
        }
        prevPositionX = transform.position.x;
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