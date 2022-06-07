using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip Clip;
    [Range(0,1f)]
    public float Volume = 1;
    [Range(0.1f,3f)]
    public float Pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;
}
