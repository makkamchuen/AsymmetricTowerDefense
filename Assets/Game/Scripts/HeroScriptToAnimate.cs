using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class HeroScriptToAnimate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private bool hasWeapon = false;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask layerMask;
    private Camera _mainCamera;

    // Use this for initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
         var movement = Input.GetAxis("Horizontal");
         
         transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
        CheckOnMobileTouch();
        CheckOnRightClick();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("HasWeapon", !animator.GetBool("HasWeapon"));
        }

        if (Input.GetKeyDown(KeyCode.R) && animator.GetBool("HasWeapon"))
        {
            animator.SetTrigger("HeroAttack");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("HeroDead");
        }
        RestrictHeroRotation();
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
    
    private void RestrictHeroRotation()
    {
        Quaternion transformRotation = transform.rotation;
        transform.rotation = new Quaternion(
            transformRotation.x,
            transformRotation.y >= 0 ? 0f : -180f,
            transformRotation.z,
            transformRotation.w
        );
    }
}