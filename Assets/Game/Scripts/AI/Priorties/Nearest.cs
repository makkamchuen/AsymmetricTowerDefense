using UnityEngine;

[CreateAssetMenu(menuName = "AI/Priority/Nearest")]
public class Nearest : Priority
{
    public override Actor Compare(Actor source, Actor target1, Actor target2)
    {
      Vector3 sourcePosition = source.transform.position;
      return Vector3.Distance(target1.transform.position, sourcePosition) <=
             Vector3.Distance(target2.transform.position, sourcePosition) 
        ? target1
        : target2;
    }
}
