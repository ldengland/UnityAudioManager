using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotCube : Interactable
{
    
    protected override void Interact()
    {
        base.Interact();
        Debug.Log("Interacting with " + transform.name);
        AudioManager.instance.PlayAudio(AudioReference.instance.metallicOneShot.referenceName);
    }
}
