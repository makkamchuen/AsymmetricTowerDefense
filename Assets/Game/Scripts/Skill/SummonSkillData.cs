using Game.Scripts;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Summon")]
public class SummonSkillData : SkillData
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private int spawnSize;
    [SerializeField] private int reachableDistance;
    private int spawnnedCount = 0;

    public override void Cast(Actor user, Vector3 destination)
    {
        GameObject gameObject = PoolManager.Spawn(_gameObject, destination);
        gameObject.transform.LookAt(destination);
        spawnnedCount++;
    }

    public override bool CanApply(Actor user, Actor targetActor)
    {
        return spawnnedCount >= spawnSize && Vector3.Distance(user.transform.position, targetActor.transform.position) <= reachableDistance - 1;
    }
}
