using UnityEngine;

public class SkillData : ScriptableObject
{
    public void Cast(Actor user, Vector3 destination)
    {
        
    }

    public bool InRange(Actor user, Actor targetActor)
    {
        return true;
    }
}
