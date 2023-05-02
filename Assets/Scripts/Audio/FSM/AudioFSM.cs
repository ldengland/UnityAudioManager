using UnityEngine;

/// <summary>
/// The possible states of an AudioFSM
/// </summary>
public enum AudioState
{
    Initializing,
    Playing,
    Stopping,
    Stopped
}

public class AudioFSM
{
    private AudioSource audioSource;
    private AudioState state;

    /// <summary>
    /// AudioFSM constructor
    /// </summary>
    /// <param name="audioSource">A Unity AudioSource</param>
    /// <param name="initialState">The inital state of the FSM</param>
    public AudioFSM(AudioSource audioSource, AudioState initialState = AudioState.Stopped)
    {
        this.audioSource = audioSource;
        state = initialState;
    }

    /// <summary>
    /// Check the current state of the FSM and perform the appropriate action
    /// </summary>
    public void Update()
    {
        switch (state)
        {
            case AudioState.Initializing:
                audioSource.Play();
                state = AudioState.Playing;
                break;
                
            case AudioState.Playing:                
                if (!audioSource.isPlaying)
                {
                    state = AudioState.Stopped;
                }
                break;
                
            case AudioState.Stopping:
                audioSource.Stop();
                state = AudioState.Stopped;
                break;
                
            case AudioState.Stopped:
                break;
        }
    }

    /// <summary>
    /// Change the FSM state to Playing
    /// </summary>
    public void Play()
    {
        if (state == AudioState.Playing)
        {
            return;
        }
        
        state = AudioState.Initializing;
    }

    /// <summary>
    /// Change the FSM state to Stopping
    /// </summary>
    public void Stop()
    {
        state = AudioState.Stopping;
    }

    /// <summary>
    /// Check if the FSM is in a Playing state
    /// </summary>
    /// <returns>True if the FSM is Playing else False</returns>
    public bool IsPlaying()
    {
        return state == AudioState.Playing;
    }
}