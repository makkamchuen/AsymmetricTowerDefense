using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatrolPattern : ScriptableObject
{
  public abstract void Move (StateController controller);
}