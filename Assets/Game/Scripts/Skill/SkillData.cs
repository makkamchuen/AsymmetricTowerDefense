using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public float cooldown;
    public abstract void Cast(Actor user, Vector3 destination);
    public abstract bool InRange(Actor user, Actor targetActor);
}
