using Game.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using TouchPhase = UnityEngine.TouchPhase;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Player _target;
    private int _numOTreasuresCollected;

    private Camera _mainCamera;

    // Use this for initialization
    private void Start()
    {
        _target = GetComponent<Player>();
        _mainCamera = Camera.main;
        _numOTreasuresCollected = 0;
    }

    private void Update()
    {
        // CheckOnMobileTouch();
        if (_target.GetStatus().Controllable())
        {
            CheckControl();
        }
        else if (Input.GetKeyDown(KeyCode.X)) { }
    }

    private void CheckControl()
    {
        CheckOnRightClick();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }
        _target.Skill.PlayAttackAnimation(hit.point);
    }

    // private void CheckOnMobileTouch()
    // {
    //     if (Input.touchCount <= 0 || Input.touches[0].phase != TouchPhase.Began)
    //     {
    //         return;
    //     }
    //
    //     Ray ray = _mainCamera.ScreenPointToRay(Input.touches[0].position);
    //     if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
    //     {
    //         return;
    //     }
    //
    //     MoveTo(hit.point);
    // }

    private void CheckOnRightClick()
    {
        if (!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            return;
        }

        MoveTo(hit.point);

    }

    private void MoveTo(Vector3 position)
    {
        _target.GetMover().MoveTo(position);
    }
}