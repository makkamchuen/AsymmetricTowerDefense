using System;
using System.Collections.Generic;
using Game.Scripts;
using Game.Scripts.Skill;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Summon")]
public class SummonSkillData : SkillData, IMaxCastApply
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int reachableDistance;
    [SerializeField] private float minDistToTarget;
    [SerializeField] private float maxDistToTarget;
    
    [SerializeField] private SummonSkillDataAtLevel[] skillDataAtLevel;
    private SortedDictionary<int, SummonSkillDataAtLevel> skillDataAtLevelDictionary;
  
    public SummonSkillDataAtLevel CurrentSkillData
    {
        get
        {
            if (skillDataAtLevelDictionary == null)
            {
                SummonSkillDataAtLevel prevLevelData = skillDataAtLevel[0];
                SummonSkillDataAtLevel nextLevelData = skillDataAtLevel[skillDataAtLevel.Length > 1 ? 1 : 0];
                int nextLevelToAdvance = nextLevelData.Level + 1;
                int nextArrayIndex = 1;
                this.skillDataAtLevelDictionary = new SortedDictionary<int, SummonSkillDataAtLevel>();
                for (int i = 1; i <= 100; i++)
                {
                    if (i == nextLevelToAdvance)
                    {
                        prevLevelData = nextLevelData;
                        nextLevelData = skillDataAtLevel[++nextArrayIndex];
                        nextLevelToAdvance = nextLevelData.Level + 1;
                    }

                    float multiplier = Convert.ToSingle(i - prevLevelData.Level) / (nextLevelData.Level - prevLevelData.Level);
                    if (multiplier is Single.NaN || multiplier is Single.PositiveInfinity) multiplier = 0;

                    int maxNumOfCast = prevLevelData.MaxNumOfCast + (nextLevelData.MaxNumOfCast - prevLevelData.MaxNumOfCast) * Convert.ToInt32(multiplier);
                    float cooldown = prevLevelData.Cooldown + (nextLevelData.Cooldown - prevLevelData.Cooldown) * multiplier;
                    SummonSkillDataAtLevel data = new SummonSkillDataAtLevel(i, maxNumOfCast, cooldown);
                    this.skillDataAtLevelDictionary.Add(i, data);
                }
            }

            return skillDataAtLevelDictionary[Statistic.CurrentLevel];
        }
    }


    public override void Cast(Actor user)
    {
        var center = user.GetComponent<AI>().GetTargetActor().transform.position;
        var destination = Utils.GetRandomPoint(center, minDistToTarget, maxDistToTarget);
        GameObject gameObject = PoolManager.Spawn(_gameObject, destination);
        gameObject.transform.LookAt(destination);
    }

    public override bool CanApply(Actor user, Actor targetActor)
    {
        if (!CanTakeDownTarget(targetActor) || !IsTarget(targetActor.tag)) return false;

        return Vector3.Distance(user.transform.position, targetActor.transform.position) <= reachableDistance - 1;
    }

    public override float GetCooldown()
    {
        return CurrentSkillData.Cooldown;
    }

    public int GetMaxNumberOfCast()
    {
        return CurrentSkillData.MaxNumOfCast;
    }
}
