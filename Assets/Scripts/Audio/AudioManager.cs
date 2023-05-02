using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    public AudioReference audioReference;

    private Dictionary<string, AudioFSM> audioFSMs = new Dictionary<string, AudioFSM>();

    /// <summary>
    /// Singleton pattern
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("AudioManager already exists!");
            Destroy(this);
        }
    }

    /// <summary>
    ///  Built-in Monobehavior function. Called on Game Start.
    /// </summary>
    void Start()
    {
        InitializeAudioStateMachines();
    }

    /// <summary>
    /// Built-in Monobehavior function. Called on each frame.
    /// </summary>
    private void Update()
    {
        foreach (AudioFSM fsm in audioFSMs.Values)
        {
            fsm.Update();
        }
    }

    /// <summary>
    /// Initializes all AudioFSMs in the AudioReference List.
    /// </summary>
    private void InitializeAudioStateMachines()
    {
        foreach (AudioFSMData fsmData in audioReference.GetAudioFSMData())
        {
            
            GameObject audioObject = null;

            // Lookup GameObject if sourceObjectName given
            if (!string.IsNullOrEmpty(fsmData.sourceObjectName))
            {
                
                audioObject = GameObject.Find(fsmData.sourceObjectName);

                if (audioObject == null)
                {
                    Debug.LogError("Audio source object not found: " + fsmData.sourceObjectName);
                }
            }

            if (audioObject == null)
            {
                audioObject = new GameObject(fsmData.clip.name);
                audioObject.transform.SetParent(transform);
                audioObject.transform.localPosition = Vector3.zero;
            }

            AudioSource source = audioObject.AddComponent<AudioSource>();
            
            // Set audio source properties
            source.clip = fsmData.clip;
            source.volume = fsmData.volume;
            source.loop = fsmData.loop;

            // Add AudioFSM to dictionary
            audioFSMs.Add(fsmData.referenceName, new AudioFSM(source, fsmData.initialState));
        }
    }

    /// <summary>
    /// Play audio clip by reference name
    /// </summary>
    /// <param name="referenceName">The reference name of a given FSM</param>
    public void PlayAudio(string referenceName)
    {
        if (audioFSMs.ContainsKey(referenceName))
        {
            audioFSMs[referenceName].Play();
        }
        else
        {
            Debug.LogError("Audio clip not found for reference: " + referenceName);
        }
    }

    /// <summary>
    /// Stop audio clip by reference name
    /// </summary>
    /// <param name="referenceName">The reference name of a given FSM</param>
    public void StopAudio(string referenceName)
    {
        if (audioFSMs.ContainsKey(referenceName))
        {
            audioFSMs[referenceName].Stop();
        }
        else
        {
            Debug.LogError("Audio clip not found for reference: " + referenceName);
        }
    }

    /// <summary>
    /// Check if audio clip is playing by reference name
    /// </summary>
    /// <param name="referenceName">The reference name of a given FSM</param>
    /// <returns> True if the FSM is in a Playing state, else False</returns>
    public bool IsPlaying(string referenceName)
    {
        if (audioFSMs.ContainsKey(referenceName))
        {
            return audioFSMs[referenceName].IsPlaying();
        }
        else
        {
            Debug.LogError("Audio clip not found for reference: " + referenceName);
            return false;
        }
    }
}