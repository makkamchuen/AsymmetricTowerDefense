using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
  public Stats baseStats;
  public float currentHealth;
  public Skill attack;
  public Status status = new Status();

  private void Start()
  {
    currentHealth = baseStats.maxHealth;
  }

  private void hit(float damage)
  {
    
  }
}