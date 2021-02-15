using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Decisions/Flee Remain")]
public class FleeRemainDecision : Decision {

  public override bool Decide(StateController controller)
  {
    if (controller.target is Minion minion)
    {
      return Remain(minion);
    }

    return false;
  }

  private bool Remain(Minion ai)
  {
    return !ai.GetMover().GetNavMeshAgent().isStopped;
  }
}