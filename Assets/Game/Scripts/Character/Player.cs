using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public Mover mover;
    public Skill[] skills;
    
    private void Start()
    {
        mover = GetComponent<Mover>();
    }
}
