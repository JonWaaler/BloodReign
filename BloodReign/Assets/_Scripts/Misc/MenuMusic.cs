using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {
    public SoundManager soundManager;

	// Use this for initialization
	void Start () {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.setLooping(Sounds.SoundName.Menu_Music, true);
        soundManager.Play(Sounds.SoundName.Menu_Music);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
