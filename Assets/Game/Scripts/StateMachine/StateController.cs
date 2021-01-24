using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

  public State currentState;
  public Stats stats;
  public State remainInState;

  [HideInInspector] public NavMeshAgent navMeshAgent;
  [HideInInspector] public GameObject target;
  [HideInInspector] public float stateTimeElapsed;

  private bool isEnable;

  private void Start() 
  {
    navMeshAgent = GetComponent<NavMeshAgent> ();
  }

  void Update()
  {
    currentState.UpdateState (this);
  }

  public void TransitionToState(State nextState)
  {
    if (nextState != remainInState) 
    {
      currentState = nextState;
      OnExitState ();
    }
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = currentState.sceneGizmoColor;
    Gizmos.DrawWireSphere (this.transform.position, 8);
    Gizmos.DrawWireSphere (this.transform.position, 5);
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