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
    if (ai.GetAttackSkill().CanHit(ai.GetTargetActor())) 
    {
      ai.GetAttackSkill().Cast(ai.GetTargetActor().transform.position);
    }
  }
}