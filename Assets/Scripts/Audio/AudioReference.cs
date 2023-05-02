using System.Collections.Generic;
using UnityEngine;

public class AudioReference : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public AudioFSMData redFloorMusic { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public AudioFSMData windAmbience { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public AudioFSMData sonarBlip { get; private set; }
    [field: SerializeField] public AudioFSMData metallicOneShot { get; private set; }


    public static AudioReference instance { get; private set; }

    private List<AudioFSMData> audioFSMs = new List<AudioFSMData>();

    /// <summary>
    /// Singleton pattern
    /// </summary>
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one AudioContainer!");
        }
        
        instance = this;

        if (audioFSMs.Count == 0)
        {
            
            // Using reflection to get all AudioFSMData public members and
            // add them to the audioFSMs list.
            foreach (var property in GetType().GetProperties())
            {
                if (property.PropertyType == typeof(AudioFSMData) && property.GetValue(this) != null)
                {
                    //Set AudioFSMData referenceName to property.Name
                    ((AudioFSMData)property.GetValue(this)).referenceName = property.Name;
                    
                    audioFSMs.Add((AudioFSMData)property.GetValue(this));
                }
            }
        }

        Debug.Log("AudioFSMs Count: " + audioFSMs.Count);
    }

    /// <summary>
    /// Getter for the List of AudioFSMData objects.
    /// </summary>
    public List<AudioFSMData> GetAudioFSMData()
    {
        return audioFSMs;
    }

}
