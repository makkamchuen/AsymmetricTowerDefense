using System;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

  public State currentState;
  [HideInInspector] public AI ai;
  public State remainInState;
  public bool isEnable = true;
  
  [HideInInspector] public float stateTimeElapsed;

  private void Start()
  {
    ai = GetComponent<AI>();
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
}