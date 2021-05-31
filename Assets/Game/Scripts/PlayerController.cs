using System;
using System.Linq;
using Game.Scripts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using TouchPhase = UnityEngine.TouchPhase;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    private Player _player;
    private Skill _skill;
    private bool _attackWhenStop = false;
    private NavMeshAgent _agent;
    private int _numOTreasuresCollected;

    private Camera _mainCamera;

    // Use this for initialization
    private void Start()
    {
        _player = GetComponent<Player>();
        _skill = GetComponent<Skill>();
        _agent = GetComponent<NavMeshAgent>();
        _mainCamera = Camera.main;
        _numOTreasuresCollected = 0;
    }
    
    

#if UNITY_STANDALONE

    private void Update()
    {
        if (_player.GetStatus().Controllable())
        {
            CheckControl();
        }
        else if (Input.GetKeyDown(KeyCode.X)) { }
    }


    private void ConditionallyMove()
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

        if (!IsCloseToCurrentPosition(hit.point))
        {
            _player.GetMover().MoveTo(hit.point);
        }
        _attackWhenStop = true;

    }


#elif UNITY_ANDROID

    private void Update()
    {
        if (_player.GetStatus().Controllable())
        {
            CheckControl();
        }
    }

    private void ConditionallyMove()
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

        if (!IsCloseToCurrentPosition(hit.point))
        {
            _player.GetMover().MoveTo(hit.point);
        }
        _attackWhenStop = true;
    }

#endif
    
    
    private void CheckControl()
    {
        ConditionallyMove();

        if (Input.GetKeyDown(KeyCode.R) || (_attackWhenStop && _agent.isStopped))
        {
            if (IsTargetInCollider())
                Attack();
            _attackWhenStop = false;
        }
    }
    
    private bool IsCloseToCurrentPosition(Vector3 point)
    {
        return Vector3.Distance(gameObject.transform.position, point) < 0.7f;
    }


    private void Attack()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }
        _player.Skill.PlayAttackAnimation(hit.point);
    }
    
    private bool IsTargetInCollider()
    {

        SlashSkillData slashSkillData = (_skill.SkillDataToUse as SlashSkillData);
        var hitBoxWidth = slashSkillData.hitBoxWidth;
        var hitBoxHeight = slashSkillData.hitBoxHeight;
        float xOffset = hitBoxWidth / 2;

        Collider[] colliders = Physics.OverlapBox(
            _player.transform.position + new Vector3(_player.GetSpriteRender().flipX? xOffset * -1 : xOffset, 0, 0),
            new Vector3(hitBoxWidth, _player.transform.localScale.y, hitBoxHeight));
        foreach (Collider collider in colliders)
        {
            Actor target = collider.GetComponent<Actor>();

            if (_skill.SkillDataToUse.CanApply(_player, target))
                return true;
        }

        return false;
    }

    
}