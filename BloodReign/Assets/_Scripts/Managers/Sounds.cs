
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds {

    public enum SoundName { Shot_Pistol, Shot_Shotgun, Shot_Sniper, Reload_Shotgun, Game_Music, Menu_Music};
    public SoundName soundName;
    public AudioClip clip;

    [Range(0, 1)]
    public float vol = 1;
    [Range(.1f, 3)]
    public float pitch = 1;


    public AudioSource source;
}
