using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  [SerializeField] private Stats baseStats;
  [SerializeField] private Skill attack;
  [SerializeField] private string weapon;
  [SerializeField] private bool faceRight = true;
  [SerializeField] private Flee flee;
  private SpriteRenderer _spriteRenderer;
  private Collider _collider;
  private Health _health;
  private Status _status;
  private bool _isFacingRight = false;

  protected virtual void Start()
  {
    _status = new Status();
    _collider = GetComponent<Collider>();
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

  public Flee GetFlee()
  {
    return flee;
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
  
  public Collider GetCollider()
  {
    return _collider;
  }

  public string GetWeapon()
  {
    return weapon;
  }
  
  public void SetIsFacingRight(bool isFacing)
  {
    _isFacingRight = isFacing;
  }
  
  private void RestrictRotation()
  {
    _spriteRenderer.flipX = faceRight? !_isFacingRight: _isFacingRight;
  }

}