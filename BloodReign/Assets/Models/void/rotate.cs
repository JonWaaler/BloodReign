using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	public GameObject darknessPit;
	
	// Update is called once per frame
	void Update () {

		darknessPit.transform.Rotate(0,0,0.05f);
	}
}
