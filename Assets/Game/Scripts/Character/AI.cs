using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Actor
{
  private Actor _target;
  [SerializeField] private Sight[] sights;

  protected override void Start()
  {
    base.Start();
    _target = this;
  }

  public Actor GetTargetActor()
  {
    return _target;
  }

  public void SetTargetActor(Actor target)
  {
    _target = target;
  }

  public Sight[] GetSights()
  {
    return sights;
  }
}
