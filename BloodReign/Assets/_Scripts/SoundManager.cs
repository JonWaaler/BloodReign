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
        //f_PistolShot = FMODUnity.RuntimeManager.CreateInstance(PistolShot);
        //f_PistolShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //f_ShotgunShot = FMODUnity.RuntimeManager.CreateInstance(ShotgunShot);
        //f_ShotgunShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //f_SniperShot = FMODUnity.RuntimeManager.CreateInstance(SniperShot);
        //f_SniperShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));

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
            sound.EventName = FMODUnity.RuntimeManager.CreateInstance(sound.SoundName);
            sound.EventName.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        }
	}
	
    public void SetPitch(string soundName, float newPitch)
    {
        foreach (Sounds sound in sounds)
        {
            if (sound.SoundName == soundName)
            {
                sound.EventName.setPitch(newPitch);
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
                float pitch;
                sound.EventName.getPitch(out pitch, out pitch);
                print("Recieved Pitch:" + pitch);
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
                sound.EventName.setVolume(newVolume);
                return;
            }
        }
    }

    public void SetVolume_ALL(float newVolume)
    {
        foreach (Sounds sound in sounds)
        {
            sound.EventName.setVolume(newVolume);
        }
    }

    public void Play(string soundName)
    {
        soundName = "event:/" + soundName;
        foreach (Sounds sound in sounds)
        {
            if (sound.SoundName == soundName)
            {
                sound.EventName.start();
                return;
            }
        }
    }
}
