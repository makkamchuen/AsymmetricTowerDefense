using System;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

  public State currentState;
  [HideInInspector] public AI target;
  public State remainInState;
  public bool isEnable = true;
  
  [HideInInspector] public float stateTimeElapsed;

  private void Start()
  {
    target = GetComponent<AI>();
  }

  void Update()
  {
    if (isEnable)
    {
      currentState.UpdateState(this);
    }
  }

  public void TransitionToState(State nextState)
  {
    if (nextState != remainInState) 
    {
      currentState = nextState;
      OnExitState ();
    }
  }

  public bool CheckIfCountDownElapsed(float duration)
  {
    stateTimeElapsed += Time.deltaTime;
    return (stateTimeElapsed >= duration);
  }

  private void OnExitState()
  {
    stateTimeElapsed = 0;
  }

  private void OnDrawGizmos()
  {
    if (currentState != null) 
    {
      Gizmos.color = currentState.sceneGizmoColor;
      Gizmos.DrawCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
    }
  }
}