using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    [SerializeField] protected FlyOrGround _flyOrGround = FlyOrGround.Fly | FlyOrGround.Ground;
    [TagField, SerializeField] protected string[] _targetTags;
    public bool IsTarget(string tag)
    {
        return Array.IndexOf(_targetTags, tag) > -1;
    }

    public HashSet<string> GetTargetSet()
    {
        HashSet<string> tagSet = new HashSet<string>();
        foreach (string tag in _targetTags)
        {
            tagSet.Add(tag);
        }

        return tagSet;
    }

    public FlyOrGround FlyOrGround => _flyOrGround;

    public abstract void Cast(Actor user); // for slash it hits and damage target.  For shooting, it spawns arrows.  For spawn, it spawns actors

    public abstract bool CanApply(Actor user, Actor targetActor);

    public virtual float GetMinDistance()
    {
        return -1;
    }

    public bool CanTakeDownTarget(Actor targetActor)
    {
        return ((int) targetActor.GetBaseStats().FlyOrGround & (int) FlyOrGround) != 0;
    }

    public abstract float GetCooldown();
    
    

}
