
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds {

    public enum SoundName { Air_Shot, Earth_Shot, Lightning_Shot, Fire_Shot, Reload_Shotgun, Game_Music, Menu_Music, Menu_Move, Menu_Ready };
    public SoundName soundName;
    public AudioClip clip;

    [Range(0, 1)]
    public float vol = 1;
    [Range(.1f, 3)]
    public float pitch = 1;


    public AudioSource source;
}
