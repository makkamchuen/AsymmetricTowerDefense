using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    [SerializeField] private float _cooldown;
    public abstract void Cast(Actor user, Vector3 destination);
    public abstract bool InRange(Actor user, Actor targetActor);
    
    public float GetCoolDown()
    {
        return _cooldown;
    }
}
