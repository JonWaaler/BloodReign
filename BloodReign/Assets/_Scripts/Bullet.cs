using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public string ID;
	public float speed;
    private TrailRenderer trail;

    //[HideInInspector]
    public float Damage;

	// Use this for initialization
	void Start () {
	}

    private void OnEnable()
    {
        if(trail == null)
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        trail.Clear();
    }
    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}
}

