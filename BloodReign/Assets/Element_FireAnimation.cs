using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_FireAnimation : MonoBehaviour {
    public Vector3 rot;
    public int numObjects = 12;
    public int curObjects;      // stores the last value of the magazine clip
    public float radius = 2;
    public List<Transform> orbitingObjects;
    public bool useThisShit = true;
    public GunBehavior gunBehavior;

    void Start()
    {
        if (useThisShit)
        {
            curObjects = gunBehavior.BulletsInMag;
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
        if (useThisShit)
        {

            if (gunBehavior.BulletsInMag != curObjects)
            {
                // when bulletsinmag goes down, we will want to up date and set curObjects
                // to the new value
                /* 
                 * 
                 * 
                 * 
                 */
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
                //print(gunBehavior.BulletsInMag);
                float ang = (360f) / gunBehavior.BulletsInMag;
                Vector3 center = transform.position;

                for (int i = 0; i < gunBehavior.BulletsInMag; i++)
                {
                    Vector3 pos = RandomCircle(center, radius, i * ang);
                    Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center - pos);
                    orbitingObjects[i].position = pos;
                }

                curObjects = gunBehavior.BulletsInMag;
            }
        }
    }

    private void FixedUpdate()
    {
        if(useThisShit)
        transform.Rotate(rot * Time.deltaTime);
        //transform.Rotate(Vector3.up * 90 * Time.deltaTime, Space.Self);
    }
}
