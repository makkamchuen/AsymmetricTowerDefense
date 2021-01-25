using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [HideInInspector] public Mover mover;
    [HideInInspector] public Skill[] skills;
    
    protected override void Start()
    {
        base.Start();
        mover = GetComponent<Mover>();
    }
}
