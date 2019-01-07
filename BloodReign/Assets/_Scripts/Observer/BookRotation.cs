using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRotation : MonoBehaviour {

    private float rotateTime = 0f;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, rotateTime, transform.rotation.eulerAngles.z);
        rotateTime += Time.deltaTime * 100f;
    }
}
