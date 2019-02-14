using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {
    public SoundManager soundManager;

	// Use this for initialization
	void Start () {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.setLooping(Sounds.SoundName.Game_Music, true);
        soundManager.Play(Sounds.SoundName.Game_Music);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
