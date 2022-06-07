using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in Sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.Clip;

            s.audioSource.pitch = s.Pitch;
            s.audioSource.volume = s.Volume;
            s.audioSource.loop = s.loop;
        }
    }
    private void Start()
    {
        //Play Theme Song
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Couldn't find sound " + s);
            return;
        }

        //If the sound is a one shot sound then vary the pitch so the user doesn't get sick of the sound
        if(s.loop == false)
        {
            s.Pitch = UnityEngine.Random.Range(0.8f, 1.2f);//Written this way because using system has its own random class

        }
        s.audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
