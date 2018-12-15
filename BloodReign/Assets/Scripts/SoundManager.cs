using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {


    public static SoundManager instance;

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

	}
	
    public void SetPitch(string soundName, float newPitch)
    {

    }

    public void SetVolume_One(string soundName, float newVolume)
    {

    }

    public void SetVolume_ALL(float newVolume)
    {

    }

    public void Play(string soundName)
    {

    }
}
