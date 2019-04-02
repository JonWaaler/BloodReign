using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public string ID;
	public float speed;
    public float lifeTime = 5;
    private float t_LifeTimer = 0;
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
        t_LifeTimer = 0;
    }
    private void OnDisable()
    {
        trail.Clear();
    }

    // Update is called once per frame
    void FixedUpdate () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);

        t_LifeTimer += Time.deltaTime;

        if (t_LifeTimer >= lifeTime)
            gameObject.SetActive(false);
	}
}

