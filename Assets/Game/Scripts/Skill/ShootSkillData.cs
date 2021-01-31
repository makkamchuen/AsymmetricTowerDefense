using System;
using Game.Scripts;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Shoot")]
public class ShootSkillData : SkillData
{
  [SerializeField] private GameObject _gameObject;
  [SerializeField] private AttackInfo _attackInfo;
  private Projectiles _projectiles;

  private void OnEnable()
  {
    _projectiles = _gameObject.GetComponent<Projectiles>();
  }

  public override void Cast(Actor user, Vector3 destination)
  {
    GameObject gameObject = PoolManager.Spawn(_gameObject, user.transform.position);
    Projectiles projectiles = gameObject.GetComponent<Projectiles>();
    projectiles.InitDirection((destination - user.transform.position).normalized);
    projectiles.SetTargets(_attackInfo.GetTargetSet());
    projectiles.AddDamage(user.GetBaseStats().attackDamage + _attackInfo.GetDamage());
  }

  public override bool InRange(Actor user, Actor targetActor)
  {
    return Vector3.Distance(user.transform.position, targetActor.transform.position) <= _projectiles.GetRange() - 1;;
  }
}