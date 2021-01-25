using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Decisions/Look")]
public class LookDecision : Decision {

  public override bool Decide(StateController controller)
  {
    bool targetVisible = Look(controller.ai);
    return targetVisible;
  }

  private bool Look(AI ai)
  {
    foreach (Sight sight in ai.sights)
    {
      if (sight.SearchTarget(ai))
      {
        return true;
      }
    }
    return false;
  }
}