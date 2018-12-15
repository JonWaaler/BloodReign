using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

    public static AudioClip playershootSound;
    static AudioSource audioSrc;

    // Use this for initialization
    void Start()
    {
        playershootSound = Resources.Load<AudioClip>("playerShoots");
        audioSrc = GetComponent<AudioSource> ();
    }



    // Update is called once per frame
    void Update(){

    }

    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "playerShoots":
                audioSrc.PlayOneShot(playershootSound);
                break;
        }
    }
}

