using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    
    public abstract void Cast(Actor user); // for slash it hits and damage target.  For shooting, it spawns arrows.  For spawn, it spawns actors
    
    public abstract bool CanApply(Actor user, Actor targetActor);

    public virtual float GetMinDistance()
    {
        return -1;
    }

    public abstract float GetCooldown();

}
