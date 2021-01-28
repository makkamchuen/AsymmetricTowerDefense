using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool _isFacingRight;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPos;
    
    protected override void Start()
    {
        base.Start();
        // navMeshAgent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        GetAnimator().SetBool(AnimationTrigger.run, false);
    }

    private void Update()
    {
        RestrictRotation();
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
        GetAnimator().SetBool(AnimationTrigger.run, true);
        if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            return;
        }
        _isFacingRight = hit.position.x > this.transform.position.x;
        navMeshAgent.destination = hit.position;
        navMeshAgent.isStopped = false;
        GetActionScheduler().StartAction(this);
    }

    private void RestrictRotation()
    {
        _spriteRenderer.flipX = !_isFacingRight;
    }
    
    private void UpdateAction()
    {
        if (GetActionScheduler().GetCurrentAction() is Mover mover && 
            mover == this &&
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