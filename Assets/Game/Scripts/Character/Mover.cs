using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool _isFacingRight;
    private SpriteRenderer _spriteRenderer;

    protected void Start()
    {
        base.Start();
        // navMeshAgent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public SpriteRenderer GetSpriteRenderer()
    {
        return _spriteRenderer;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }

    void Update()
    {
        RestrictRotation();
        navMeshAgent.enabled = GetActor().GetCurrentHealth() > 0;
    }

    public void MoveTo(Vector3 destination)
    {
        if (!NavMesh.SamplePosition(destination, out NavMeshHit hit, Mathf.Infinity, NavMesh.AllAreas))
        {
            return;
        }
        _isFacingRight = hit.position.x > this.transform.position.x;
        navMeshAgent.destination = hit.position;
        navMeshAgent.isStopped = false;
    }

    private void RestrictRotation()
    {
        _spriteRenderer.flipX = !_isFacingRight;
    }

    public override void Cancel()
    {
        navMeshAgent.isStopped = true;
    }
}