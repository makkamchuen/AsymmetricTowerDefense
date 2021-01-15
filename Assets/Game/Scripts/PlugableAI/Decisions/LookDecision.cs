using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision {

  public override bool Decide(StateController controller)
  {
    bool targetVisible = Look(controller);
    return targetVisible;
  }

  private bool Look(StateController controller)
  {
    foreach (Sight sight in controller.stats.sights)
    {
      if (sight.SearchTarget(controller))
      {
        return true;
      }
    }
    return false;
  }
}