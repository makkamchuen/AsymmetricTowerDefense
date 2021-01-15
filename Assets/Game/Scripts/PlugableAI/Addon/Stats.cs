using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Addon/Stats"), Serializable]
public class Stats : ScriptableObject
{

  public Sight[] sights;
  public PatrolPattern patrolPattern;
}
