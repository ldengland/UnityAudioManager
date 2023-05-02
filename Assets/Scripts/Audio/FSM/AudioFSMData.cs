using UnityEngine;

[CreateAssetMenu(fileName = "AudioFSMData", menuName = "Audio FSM Data", order = 30)]
public class AudioFSMData : ScriptableObject
{
    [HideInInspector]
    public string referenceName;
    private AudioSource audioSource;

    [Header("Audio Clip Settings")]
    public AudioClip clip;
    public AudioType audioType;
    public AudioState initialState;
    public float volume = 1.0f;
    public bool loop = false;

    [Header("3D Audio Settings")]
    public string sourceObjectName;
}