using Game.Scripts;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Summon")]
public class SummonSkillData : SkillData
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int spawnSize;
    [SerializeField] private int reachableDistance;

    public override void Cast(Actor user, Vector3 destination)
    {
        GameObject gameObject = PoolManager.Spawn(_gameObject, destination);
        gameObject.transform.LookAt(destination);
        spawnSize--;
    }

    public override bool CanApply(Actor user, Actor targetActor)
    {
        return spawnSize > 0 && Vector3.Distance(user.transform.position, targetActor.transform.position) <= reachableDistance - 1;
    }
}
