using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Skill;
using UnityEngine;
using UnityEngine.AI;

public class Skill : ActorActionComponent
{
  [SerializeField] private SkillData[] skillDatas;
  private List<SkillData> skillDataList;
  private float _cooldown;
  private SkillData skillDataToUse;
  private int[] castCount;
  private float minimunSkillDistance;
  private Actor _actor;

  protected override void Start()
  {
    base.Start();
    skillDataToUse = skillDatas[0];
    skillDataList = new List<SkillData>();
    skillDataList.AddRange(skillDatas);
    castCount = new int[skillDatas.Length];
    minimunSkillDistance = skillDataList.Min(skillData => skillData.GetMinDistance());
    _actor = GetComponent<Actor>();
  }

  private void Update()
  {
    if (_cooldown != 0)
    {
      _cooldown -= Time.deltaTime;
      if (_cooldown <= 0)
      {
        _cooldown = 0;
      }
    }

  }

  public void PlayAttackAnimation(Vector3 destination)
  {
    if (!skillDataToUse || _cooldown != 0 || !GetActor().GetStatus().Attackable())
    {
      return;
    }
    if (skillDataToUse.name == "MeleeSpawn" || skillDataToUse.name == "RangeSpawn")
    {
      GetAnimator().SetTrigger(AnimationTrigger.spawn);
    }
    else
    {
      GetAnimator().SetTrigger(AnimationTrigger.attack);
    }

    GetActor().SetIsFacingRight(destination.x > transform.position.x);
    GetActionScheduler().StartAction(this);
    _cooldown = skillDataToUse.GetCooldown();
  }

  public bool CanHit(Actor target)
  {

    if (CanHitTarget(target) && !ReachSkillMaxCount()) return true;
    foreach (var skillData in skillDatas)
    {
      skillDataToUse = skillData;
      if (CanHitTarget(target) && !ReachSkillMaxCount()) return true;
    }

    skillDataToUse = skillDataList[0];
    return false;
  }

  private bool ReachSkillMaxCount()
  {
    if (skillDataToUse is IMaxCastApply)
    {
      IMaxCastApply skillDataToUseWithMaxCount = skillDataToUse as IMaxCastApply;
      int index = skillDataList.IndexOf(skillDataToUse);
      if (castCount[index] >= skillDataToUseWithMaxCount.GetMaxNumberOfCast())
      {
        return true;
      }
    }
    return false;

  }

  public override void Cancel()
  {
    GetAnimator().SetBool(AnimationTrigger.attack, false);
    // GetAnimator().SetBool(AnimationTrigger.spawn, false);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    var skill = _actor.Skill.skillDataToUse;
    if (skill is SlashSkillData)
    {
      var hitBoxWidth = (skill as SlashSkillData).hitBoxWidth;
      var hitBoxHeight = (skill as SlashSkillData).hitBoxHeight;

      Gizmos.DrawWireCube(transform.position + new Vector3(hitBoxWidth / 2 * (_actor.IsFacingRight == _actor.FaceRightByDefault? - 1 : 1), 0, 0),
        new Vector3(hitBoxWidth, transform.localScale.y, hitBoxHeight));

    }
  }

  private bool CanHitTarget(Actor target)
  {
    return skillDataToUse && skillDataToUse.CanApply(GetActor(), target);
  }

  //determine if the distance is too close and no skill can hit that close distance
  public bool IsTooCloseToHit(Actor user, Actor targetActor)
  {
    var distance = Vector3.Distance(user.transform.position, targetActor.transform.position);
    return distance < minimunSkillDistance;
  }

  public void CastSkill()
  {
    this.skillDataToUse.Cast(this.GetActor());
    int index = skillDataList.IndexOf(skillDataToUse);
    castCount[index]++;

  }

  public SkillData[] SkillDatas => skillDatas;

}