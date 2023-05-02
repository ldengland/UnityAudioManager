using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCube : Interactable
{
    protected override void Interact()
    {
        
        if(AudioManager.instance.IsPlaying(AudioReference.instance.sonarBlip.referenceName))
        {
            AudioManager.instance.StopAudio(AudioReference.instance.sonarBlip.referenceName);
        }
        else
        {
            AudioManager.instance.PlayAudio(AudioReference.instance.sonarBlip.referenceName);
        }
    }
}
