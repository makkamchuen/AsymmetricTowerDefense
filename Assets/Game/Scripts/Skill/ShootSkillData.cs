using System;
using System.Collections.Generic;
using Cinemachine;
using Game.Scripts;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Shoot")]
public class ShootSkillData : AttackSkillData
{
  [SerializeField] private GameObject _gameObject;
  

  private Projectiles _projectiles;

  private void OnEnable()
  {
    _projectiles = _gameObject.GetComponent<Projectiles>();
  }

  public override void Cast(Actor user)
  {
    var destination = user.GetComponent<AI>().GetTargetActor().transform.position;
    GameObject gameObject = Instantiate(_gameObject, user.transform.position, Quaternion.identity);
    Projectiles projectiles = gameObject.GetComponent<Projectiles>();
    projectiles.InitDirection((destination - user.transform.position).normalized);
    projectiles.SetTargets(GetTargetSet());
    projectiles.AddDamage(user.GetBaseStats().AttackDamage + GetDamage());
  }

  public override bool CanApply(Actor user, Actor targetActor)
  {
    var distance = Vector3.Distance(user.transform.position, targetActor.transform.position) + 1;
    return distance <= _projectiles.GetMaxDistance() && distance >= _projectiles.GetMinDistance();
  }
  

  public override float GetMinDistance()
  {
    return _projectiles.GetMinDistance();
  }

}