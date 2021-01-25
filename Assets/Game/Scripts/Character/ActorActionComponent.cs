using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorActionComponent : MonoBehaviour, IAction
{
  [HideInInspector] public Animator animator;
  [HideInInspector] public ActionScheduler actionScheduler;
  [HideInInspector] public Actor actor;

  protected virtual void Start()
  {
    animator = GetComponent<Animator>();
    actionScheduler = GetComponent<ActionScheduler>();
    actor = GetComponent<Actor>();
  }

  public abstract void Cancel();
}
