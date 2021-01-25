using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Decisions/Attack")]
public class AttackDecision : Decision {

  public override bool Decide(StateController controller)
  {
    return CanAttack(controller.ai);
  }

  private bool CanAttack(AI ai)
  {
    return ai.attack.CanHit(ai.target);
  }
}