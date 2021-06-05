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
  private GameObject _positionOffset;
  private Collider _collider;
  private Health _health;
  private Status _status;
  private bool _isFacingRight = false;

  protected virtual void Start()
  {
    _status = new Status();
    _collider = GetComponent<Collider>();
    _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    _positionOffset = transform.GetChild(0).gameObject;
    _health = GetComponent<Health>();
  }

  private void Update()
  {
    RestrictRotation();
  }

  public Skill Skill => attack;

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
    if (_spriteRenderer.sprite.name == "char_devil_sprite")
    {
      Transform devil = transform.GetChild(0).transform;
      if (_isFacingRight)
      {
        transform.eulerAngles = new Vector3(0f, 180f, 0f);
        devil.eulerAngles = new Vector3(-45f, 180f, 0f);
        devil.localPosition = new Vector3(devil.localPosition.x, -0.4f, 0.6f);
      }
      else
      {
        transform.eulerAngles = new Vector3(0f, 0f, 0f);
        devil.transform.eulerAngles = new Vector3(45f, 0, 0f);
        devil.localPosition = new Vector3(devil.localPosition.x, -0.4f, -0.45f);
      }
      // _spriteRenderer.flipY = faceRight?!_isFacingRight : _isFacingRight;
    }
    else
    {
      _spriteRenderer.flipX = faceRight? _isFacingRight: !_isFacingRight;
    }
  }

}