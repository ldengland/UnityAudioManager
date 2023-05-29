using UnityEngine;

/// <summary>
/// The possible states of an AudioFSM
/// </summary>
public enum AudioState
{
    Initializing,
    FadingIn,
    Playing,
    FadingOut,
    Pausing,
    Paused,
    Stopping,
    Stopped
}

public class AudioFSM
{
    private AudioSource audioSource;
    private AudioState state;
    private float fadeSpeed;

    /// <summary>
    /// AudioFSM constructor
    /// </summary>
    /// <param name="audioSource">A Unity AudioSource</param>
    /// <param name="initialState">The inital state of the FSM</param>
    public AudioFSM(AudioSource audioSource, AudioState initialState = AudioState.Stopped, float fadeSpeed = 1.0f)
    {
        this.audioSource = audioSource;

        this.fadeSpeed = fadeSpeed;
        
        // Arbitrary Defaults, Change Later
        this.audioSource.spatialBlend = 0.6f;
        this.audioSource.dopplerLevel = 0.0f;
        
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
                if (state == AudioState.Paused)
                {
                    audioSource.UnPause();
                }       
                else
                {
                    audioSource.Play();
                }
             
                state = AudioState.Playing;
                break;
                
            case AudioState.FadingIn:
                audioSource.volume += fadeSpeed * Time.deltaTime;
                if (audioSource.volume >= 1.0f)
                {
                    audioSource.volume = 1;
                    state = AudioState.Playing;
                }
                break;
                
            case AudioState.Playing:                
                if (!audioSource.isPlaying)
                {
                    state = AudioState.Stopped;
                }
                break;

            case AudioState.FadingOut:
                audioSource.volume -= fadeSpeed * Time.deltaTime;
                if (audioSource.volume <= 0.0f)
                {
                    audioSource.volume = 0;
                    state = AudioState.Stopped;
                }
                break;
                
            case AudioState.Pausing:
                audioSource.Pause();
                state = AudioState.Paused;
                break;

            case AudioState.Paused:
                break;
               
            case AudioState.Stopping:
                audioSource.Stop();
                //audioSource.clip.UnloadAudioData();
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
    /// Change the FSM state to FadingIn
    /// </summary>
    public void FadeIn()
    {
        if (state == AudioState.FadingIn || state == AudioState.Playing)
        {
            return;
        }

        audioSource.volume = 0.5f;
        audioSource.Play();

        state = AudioState.FadingIn;
    }

    /// <summary>
    /// Change the FSM state to FadingOut
    /// </summary>
    public void FadeOut()
    {
        if (state == AudioState.FadingOut || state == AudioState.Paused || state == AudioState.Stopped)
        {
            return;
        }

        state = AudioState.FadingOut;
    }

    /// <summary>
    /// Change the FSM state to Pausing
    /// </summary>
    public void Pause()
    {
        state = AudioState.Pausing;
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

    /// <summary>
    /// Check if the FSM is in a Paused state
    /// </summary>
    /// <returns></returns>
    public bool IsPaused()
    {
        return state == AudioState.Paused;
    }
}