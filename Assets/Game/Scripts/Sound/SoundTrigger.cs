using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    [SerializeField][FMODUnity.EventRef] private string attackSound;
    [SerializeField][FMODUnity.EventRef] private string weaponSound;
    [SerializeField][FMODUnity.EventRef] private string impactSound;
    [SerializeField][FMODUnity.EventRef] private string dieSound;

    public void PlayAttackSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(attackSound);
    }

    public void PlayWeaponSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(weaponSound);
    }

    public void PlayImpactSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(impactSound);
    }

    public void PlayDieSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(dieSound);
    }
}