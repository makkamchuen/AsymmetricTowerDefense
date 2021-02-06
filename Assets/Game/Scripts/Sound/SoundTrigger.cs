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
        FMODUnity.RuntimeManager.PlayOneShotAttached(spawn, gameObject);
    }

    public void PlayAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(attack, gameObject);
    }

    public void PlayWeaponSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(weapon, gameObject);
    }

    public void PlayHurtSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(hurt, gameObject);
    }

    public void PlayDeadSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(dead, gameObject);
    }

}