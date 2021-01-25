using UnityEngine;

public abstract class Priority: ScriptableObject
{ 
  public abstract Actor Compare(Actor source, Actor target1, Actor target2);
}
