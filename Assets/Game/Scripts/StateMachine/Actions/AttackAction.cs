using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/Actions/Attack")]
public class AttackAction : Action 
{
  public override void Act (AI ai)
  {
    Attack (ai);
  }

  private void Attack(AI ai)
  {
    ai.GetAttackSkill().PlayAttackAnimation(ai.GetTargetActor().transform.position);
  }
}