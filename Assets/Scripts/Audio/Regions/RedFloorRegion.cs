using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFloorRegion : MonoBehaviour
{
    public Transform playerPosition;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Detected");
        
        if (other.gameObject.tag == "Player")
        {
            if (!AudioManager.instance.IsPlaying(AudioReference.instance.redFloorMusic.referenceName))
            {
                AudioManager.instance.PlayAudio(AudioReference.instance.redFloorMusic.referenceName);
            }

            if (AudioManager.instance.IsPlaying(AudioReference.instance.windAmbience.referenceName))
            {
                AudioManager.instance.StopAudio(AudioReference.instance.windAmbience.referenceName);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (AudioManager.instance.IsPlaying(AudioReference.instance.redFloorMusic.referenceName))
            {
                AudioManager.instance.StopAudio(AudioReference.instance.redFloorMusic.referenceName);
            }

            if (!AudioManager.instance.IsPlaying(AudioReference.instance.windAmbience.referenceName))
            {
                AudioManager.instance.PlayAudio(AudioReference.instance.windAmbience.referenceName);
            }
        }
    }
}
