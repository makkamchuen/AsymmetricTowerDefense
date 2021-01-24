using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorActionComponent : MonoBehaviour
{
  public Animator animator;
  public ActionScheduler actionScheduler;
  public Actor actor;

  private void Start()
  {
    animator = GetComponent<Animator>();
    actionScheduler = GetComponent<ActionScheduler>();
    actor = GetComponent<Actor>();
  }
}
