using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _channelTime;
    public abstract void Cast(Actor user, Vector3 destination);
    public abstract bool CanApply(Actor user, Actor targetActor);

    public virtual float GetMinDistance()
    {
        return -1;
    }
    
    public float GetCoolDown()
    {
        return _cooldown;
    }
    
    public float GetChannelTime()
    {
        return _channelTime;
    }
}
