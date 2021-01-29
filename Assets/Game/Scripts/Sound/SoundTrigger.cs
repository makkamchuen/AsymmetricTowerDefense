using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField][FMODUnity.EventRef] string spawn;
    [SerializeField][FMODUnity.EventRef] string attack;
    [SerializeField][FMODUnity.EventRef] string weapon;
    [SerializeField][FMODUnity.EventRef] string hurt;
    [SerializeField][FMODUnity.EventRef] string dead;

    public void PlaySpawnSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(spawn);
    }

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