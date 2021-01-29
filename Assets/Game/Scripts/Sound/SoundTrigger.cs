using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField][FMODUnity.EventRef] private string attack;
    [SerializeField][FMODUnity.EventRef] private string weapon;
    [SerializeField][FMODUnity.EventRef] private string hurt;
    [SerializeField][FMODUnity.EventRef] private string dead;

    public void PlayAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(attack);
    }

    public void PlayWeaponSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(weapon);
    }

    public void PlayHurtSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(hurt);
    }

    public void PlayDeadSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(dead);
    }
}