using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Actor
{
  [HideInInspector] public Actor target;
  public Sight[] sights;

  protected override void Start()
  {
    base.Start();
    target = this;
  }
}
