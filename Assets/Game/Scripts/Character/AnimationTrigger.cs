using UnityEngine;

public class AnimationTrigger
{
  public static readonly int dead = Animator.StringToHash("Dead");
  public static readonly int attack = Animator.StringToHash("Attack");
  public static readonly int spawn = Animator.StringToHash("Spawn");
  public static readonly int run = Animator.StringToHash("Run");
  public static readonly int hurt = Animator.StringToHash("Hit");
}