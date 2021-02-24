using Game.Scripts;
using Game.Scripts.Skill;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Summon")]
public class SummonSkillData : SkillData, IMaxCastApply
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int maxNumOfCast;
    [SerializeField] private int reachableDistance;
    [SerializeField] private float minDistToTarget;
    [SerializeField] private float maxDistToTarget;

    public override void Cast(Actor user)
    {
        var center = user.GetComponent<AI>().GetTargetActor().transform.position;
        var destination = Utils.GetRandomPoint(center, minDistToTarget, maxDistToTarget);
        GameObject gameObject = PoolManager.Spawn(_gameObject, destination);
        gameObject.transform.LookAt(destination);
    }

    public override bool CanApply(Actor user, Actor targetActor)
    {
        return Vector3.Distance(user.transform.position, targetActor.transform.position) <= reachableDistance - 1;
    }

    public int GetMaxNumberOfCast()
    {
        return maxNumOfCast;
    }
}
