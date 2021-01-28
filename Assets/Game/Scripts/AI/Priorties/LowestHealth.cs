using UnityEngine;

[CreateAssetMenu(menuName = "AI/Priority/LowestHealth")]
public class LowestHealth : Priority
{
    public override Actor Compare(Actor source, Actor target1, Actor target2)
    {
        return target1.GetHealth().GetCurrentHealth() <= target2.GetHealth().GetCurrentHealth() ? target1 : target2;
    }
}
