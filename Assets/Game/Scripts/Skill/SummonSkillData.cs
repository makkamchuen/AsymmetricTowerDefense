using Game.Scripts;
using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Summon")]
public class SummonSkillData : SkillData
{
    [SerializeField] private GameObject _gameObject;

    public override void Cast(Actor user, Vector3 destination)
    {
        GameObject gameObject = PoolManager.Instantiate(_gameObject);
        gameObject.transform.position = user.transform.position;
        gameObject.transform.LookAt(destination);
    }

    public override bool InRange(Actor user, Actor targetActor)
    {
        return true;
    }
}
