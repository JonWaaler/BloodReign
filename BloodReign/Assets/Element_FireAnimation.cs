using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_FireAnimation : MonoBehaviour {
    public Vector3 rot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rot * Time.deltaTime);
	}
}
