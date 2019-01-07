
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sounds {
    [FMODUnity.EventRef]
    public string SoundName;
    public FMOD.Studio.EventInstance EventName;
}
