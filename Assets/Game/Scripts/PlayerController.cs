using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Player target;
    [SerializeField] private LayerMask layerMask;

    private Camera _mainCamera;

    // Use this for initialization
    private void Start()
    {
        target = GetComponent<Player>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckOnMobileTouch();
        CheckOnRightClick();
        if (Input.GetKeyDown(KeyCode.R))
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
        }
    }

    public void Attack()
    {
        // target.attack.Cast();
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

        MoveTo(hit.point);
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
        MoveTo(hit.point);
    }

    private void MoveTo(Vector3 position)
    {
        target.GetMover().MoveTo(position);
    }
}