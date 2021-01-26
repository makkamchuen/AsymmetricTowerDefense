using System;
using UnityEngine;
using UnityEngine.AI;

public class Mover : ActorActionComponent
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private bool _isFacingRight;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _lastPos;
    
    private static readonly int doDead = Animator.StringToHash("Dead");
    private static readonly int doAttack = Animator.StringToHash("Attack");
    private static readonly int doRun = Animator.StringToHash("Run");

    protected void Start()
    {
        base.Start();
        // navMeshAgent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lastPos = this.transform.parent.transform.position;
    }

    private void Update()
    {
        Vector3 curPos = this.transform.parent.transform.position;
        if (curPos != _lastPos)
        {
            /* There is an issue with the rigidBody velocity calculation
            }else if (rigidBody.velocity.sqrMagnitude != 0){
                animator.SetBool(IsMoving, true);
            }
            Below is the workaround soln which need some more work later
            */
            _lastPos = curPos;
            GetAnimator().SetBool(doRun, true);
        }
        else
        {
            GetAnimator().SetBool(doRun, false);
        }

        RestrictRotation();
        navMeshAgent.enabled = GetActor().GetCurrentHealth() > 0;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
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