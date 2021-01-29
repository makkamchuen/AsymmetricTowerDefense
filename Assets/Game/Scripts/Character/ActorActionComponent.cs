using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public abstract class ActorActionComponent : MonoBehaviour, IAction
{
  private Animator _animator;
  private ActionScheduler _actionScheduler;
  private Actor _actor;

  protected virtual void Start()
  {
    _animator = GetComponentInChildren<Animator>();
    _actionScheduler = GetComponent<ActionScheduler>();
    _actor = GetComponent<Actor>();
  }

  public abstract void Cancel();

  protected Actor GetActor()
  {
    return _actor;
  }

  protected ActionScheduler GetActionScheduler()
  {
    return _actionScheduler;
  }
  
  protected Animator GetAnimator()
  {
    return _animator;
  }
}