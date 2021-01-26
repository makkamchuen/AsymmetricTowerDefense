using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateMachine/State")]
public class State : ScriptableObject 
{

  public Action[] actions;
  public Transition[] transitions;
  public Color sceneGizmoColor = Color.grey;

  public void UpdateState(StateController controller)
  {
    DoActions (controller.target);
    CheckTransitions (controller);
  }

  private void DoActions(AI ai)
  {
    for (int i = 0; i < actions.Length; i++) {
      actions [i].Act (ai);
    }
  }

  private void CheckTransitions(StateController controller)
  {
    for (int i = 0; i < transitions.Length; i++) 
    {
      bool decisionSucceeded = transitions [i].decision.Decide (controller);

      if (decisionSucceeded) {
        controller.TransitionToState (transitions [i].trueState);
      } else 
      {
        controller.TransitionToState (transitions [i].falseState);
      }
    }
  }
}