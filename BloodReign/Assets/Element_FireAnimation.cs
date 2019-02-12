using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_FireAnimation : MonoBehaviour {
    public Vector3 rot;
    public Transform objRot;
    public int numObjects = 12;
    public float radius = 2;
    public List<Transform> orbitingObjects;

    public GunBehavior gunBehavior;
    // Use this for initialization
    void Start()
    {
        float ang = 360f / numObjects;

        Vector3 center = transform.position;
        for (int i = 0; i < numObjects; i++)
        {
            Vector3 pos = RandomCircle(center, radius, i * ang);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            orbitingObjects[i].position = pos;
        }

        // need to get the correct players gun
        //gunBehavior = 
    }

    Vector3 RandomCircle(Vector3 center, float radius, float ang)
    {
        
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }


    // Update is called once per frame
    void Update () {

        for (int i = 0; i < numObjects; i++)
        {
            // set object that got used inactive
            if (i > gunBehavior.BulletsInMag)
            {
                orbitingObjects[i].gameObject.SetActive(false);
            }
            else
                orbitingObjects[i].gameObject.SetActive(true);
        }
        print(gunBehavior.BulletsInMag);
        float ang = (360f) / gunBehavior.BulletsInMag;
        Vector3 center = transform.position;

        for (int i = 0; i < gunBehavior.BulletsInMag; i++)
        {
            Vector3 pos = RandomCircle(center, radius, i * ang);
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
            orbitingObjects[i].position = pos;
        }
    }

    private void FixedUpdate()
    {
        transform.RotateAround(transform.position ,Vector3.up ,rot.y * Time.deltaTime);
        
    }
}
