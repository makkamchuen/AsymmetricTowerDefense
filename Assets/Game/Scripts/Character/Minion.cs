using System.Collections;
using System.Collections.Generic;
using Data.Util;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Minion : AI
{
    public Mover mover;
    public PatrolPattern PatrolPattern;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }
}
