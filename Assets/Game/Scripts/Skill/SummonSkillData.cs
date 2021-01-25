using UnityEngine;

[CreateAssetMenu (menuName = "Skill/Summon")]
public class SummonSkillData : SkillData
{
    public Actor actor;

    public override void Cast(Actor user, Vector3 destination)
    {
    }

    public override bool InRange(Actor user, Actor targetActor)
    {
        return true;
    }
}
