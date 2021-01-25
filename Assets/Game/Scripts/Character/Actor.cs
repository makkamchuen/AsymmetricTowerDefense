using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  public Stats baseStats;
  [HideInInspector] public float currentHealth;
  public Skill attack;
  [HideInInspector] public Status status = new Status();

  protected virtual void Start()
  {
    currentHealth = baseStats.maxHealth;
  }

  public void Hit(float damage)
  {
    
  }
}