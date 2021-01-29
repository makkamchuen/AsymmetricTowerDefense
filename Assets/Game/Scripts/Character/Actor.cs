using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  [SerializeField] private Stats baseStats;
  [SerializeField] private Skill attack;
  private SpriteRenderer _spriteRenderer;
  private Health _health;
  private Status _status;
  private bool _isFacingRight = false;

  protected virtual void Start()
  {
    _status = new Status();
    _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    _health = GetComponent<Health>();
  }

  private void Update()
  {
    RestrictRotation();
  }

  public Skill GetAttackSkill()
  {
    return attack;
  }

  public Status GetStatus()
  {
    return _status;
  }
  
  public Stats GetBaseStats()
  {
    return baseStats;
  }

  public SpriteRenderer GetSpriteRender()
  {
    return _spriteRenderer;
  }
  
  public Health GetHealth()
  {
    return _health;
  }

  public void SetIsFacingRight(bool isFacing)
  {
    _isFacingRight = isFacing;
  }
  
  private void RestrictRotation()
  {
    _spriteRenderer.flipX = !_isFacingRight;
  }
}