﻿using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class HeroScriptToAnimate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;
    // [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float yOffset;

    private Camera _mainCamera;
    private bool _isFacingRight;
    private Vector3 _lastPos;

    private static readonly int doDead = Animator.StringToHash("Dead");
    private static readonly int doAttack = Animator.StringToHash("Attack");
    private static readonly int doRun = Animator.StringToHash("Run");
    // private static readonly int Weapon = Animator.StringToHash("Weapon");

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        // rigidBody = this.transform.parent.GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
        _lastPos = this.transform.parent.transform.position;
    }

    private void Update()
    {
        var curPos = this.transform.parent.transform.position;
        // var movement = Input.GetAxis("Horizontal");
        // transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        CheckOnMobileTouch();
        CheckOnRightClick();
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     EquipWeapon();
        // }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            SetDeath();
        }
        else if (curPos != _lastPos)
        {
            /* There is an issue with the rigidBody velocity calculation
            }else if (rigidBody.velocity.sqrMagnitude != 0){
                animator.SetBool(IsMoving, true);
            }
            Below is the workaround soln which need some more work later
            */
            _lastPos = curPos;
            animator.SetBool(doRun, true);
        }
        else
        {
            animator.SetBool(doRun, false);
        }

        RestrictRotation();
    }

    // public void EquipWeapon()
    // {
    //     animator.SetBool(Weapon, !animator.GetBool(Weapon));
    // }

    public void Attack()
    {
        animator.SetTrigger(doAttack);
    }

    private void SetDeath()
    {
        animator.SetTrigger(doDead);
    }

    private void CheckOnMobileTouch()
    {
        if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began)
        {
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Input.touches[0].position);
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }

        MoveHero(hit.point);
    }

    private void CheckOnRightClick()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }
        _isFacingRight = hit.point.x > this.transform.position.x;
        MoveHero(hit.point);
    }

    private void MoveHero(Vector3 position)
    {
        if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(hit.position);
    }

    public void RestrictRotation()
    {
        if (_isFacingRight) { GetComponent<SpriteRenderer>().flipX = false; }
        else { GetComponent<SpriteRenderer>().flipX = true; }

        /*
        Quaternion newTransformRotation = transform.rotation;
        transform.rotation = new Quaternion(
            newTransformRotation.x,
            _isFacingRight ? 0f : -180f,
            newTransformRotation.z,
            newTransformRotation.w
        );
        */
    }
}