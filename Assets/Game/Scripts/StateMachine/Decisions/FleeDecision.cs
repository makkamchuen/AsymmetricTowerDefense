using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Decisions/Flee")]
public class FleeDecision : Decision {

  public override bool Decide(StateController controller)
  {
    return ShouldFlee(controller);
  }

  private bool ShouldFlee(StateController controller)
  {
    var enemy = controller.target.GetTargetActor();
    var character = controller.target;
    return !character.GetAttackSkill().CanHit(enemy) && character.GetAttackSkill().IsTooCloseToHit(character, enemy);
  }
}