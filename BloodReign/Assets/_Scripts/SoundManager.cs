using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {


    public static SoundManager instance;
    //public AudioMixerGroup MixerGroup;
    //
    //[FMODUnity.EventRef]
    //public string PistolShot;
    //FMOD.Studio.EventInstance f_PistolShot;
    //
    //[FMODUnity.EventRef]
    //public string ShotgunShot;
    //FMOD.Studio.EventInstance f_ShotgunShot;
    //
    //[FMODUnity.EventRef]
    //public string SniperShot;
    //FMOD.Studio.EventInstance f_SniperShot;

    public Sounds[] sounds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        else
            Destroy(this);
    }

    // Use this for initialization
    void Start () {
        // Initialize all the sounds
        foreach (var sound in sounds)
        {

        }
	}
	
    public void SetPitch(string soundName, float newPitch)
    {
        foreach (Sounds sound in sounds)
        {
            if (sound.SoundName == soundName)
            {

                return;
            }
        }
    }

    public float GetPitch(string soundName)
    {
        soundName = "event:/" + soundName;
        foreach (Sounds sound in sounds)
        {
            if (sound.SoundName == soundName)
            {
                float pitch =1;


                return pitch;
            }
        }
        return 0;
    }

    public void SetVolume_One(string soundName, float newVolume)
    {
        soundName = "event:/" + soundName;
        foreach (Sounds sound in sounds)
        {
            if (sound.SoundName == soundName)
            {
                
                return;
            }
        }
    }

    public void SetVolume_ALL(float newVolume)
    {
        foreach (Sounds sound in sounds)
        {

        }
    }

    public void Play(string soundName)
    {
        soundName = "event:/" + soundName;
        foreach (Sounds sound in sounds)
        {
            if (sound.SoundName == soundName)
            {

                return;
            }
        }
    }
}
