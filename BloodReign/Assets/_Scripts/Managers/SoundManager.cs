using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;    
    public Sounds[] sounds;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.vol;
            s.source.pitch = s.pitch;
        }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
            Destroy(this);

    }

    public void Play(Sounds.SoundName name)
    {
        foreach (var s in sounds)
        {
            if(s.soundName == name)
            {
                if (s.source == null)
                {
                    Debug.LogError("Source Not Found", this);
                    return;
                }
                s.source.Play();
                return;
            }
        }

    }

    public void Stop(Sounds.SoundName name)
    {
        foreach (var s in sounds)
        {
            if (s.soundName == name)
            {
                if (s.source == null)
                {
                    Debug.LogError("Source Not Found", this);
                    return;
                }
                s.source.Stop();
                return;
            }
        }

    }

    public void setLooping(Sounds.SoundName name, bool isLooping)
    {
        foreach (var s in sounds)
        {
            if (s.soundName == name)
            {
                s.source.loop = true;
        
                return;
            }
        }
    }
	
    public void SetPitch(Sounds.SoundName name, float newPitch)
    {
        foreach (var s in sounds)
        {
            if (s.soundName == name)
            {
                s.source.Play();

                return;
            }
        }
    }

    public float GetPitch(Sounds.SoundName name)
    {
        foreach (var s in sounds)
        {
            if (s.soundName == name)
            {
                s.source.Play();

                return s.pitch;
            }
            else
                return 1;
        }
        return 1;
    }

    public void SetVolume_One(Sounds.SoundName name, float newVolume)
    {
        foreach (var s in sounds)
        {
            if (s.soundName == name)
            {
                s.vol = newVolume;

                return;
            }
        }
    }

    public void SetVolume_ALL(float newVolume)
    {
        foreach (var s in sounds)
        {
            s.vol = newVolume;
        }
    }

}
