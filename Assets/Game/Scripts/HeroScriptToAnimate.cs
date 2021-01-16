using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class HeroScriptToAnimate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;

    private Camera _mainCamera;
    private bool _isFacingRight;

    private static readonly int HeroDead = Animator.StringToHash("HeroDead");
    private static readonly int HasWeapon = Animator.StringToHash("HasWeapon");
    private static readonly int HeroAttack = Animator.StringToHash("HeroAttack");

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // var movement = Input.GetAxis("Horizontal");
        // transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        CheckOnMobileTouch();
        CheckOnRightClick();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SetDeath();
        }

        RestrictRotation();
    }

    public void EquipWeapon()
    {
        animator.SetBool(HasWeapon, !animator.GetBool(HasWeapon));
    }

    public void Attack()
    {
        if (animator.GetBool(HasWeapon))
        {
            animator.SetTrigger(HeroAttack);
        }
    }

    private void SetDeath()
    {
        animator.SetTrigger(HeroDead);
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