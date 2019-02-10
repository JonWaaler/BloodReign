using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSight : MonoBehaviour {
    // Attached to the Player container

    public LineRenderer Line;
    public Transform GunPosition;
    Vector3[] positions;
    private Vector3 lineEndPosition = Vector3.zero;


	void Update ()
    {
        RaycastHit hit;
        float dist;
        float x;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(GunPosition.position, transform.parent.forward, out hit, 25))
        {
            //lineEndPosition = hit.point;
            //dist = hit.distance;
            print("Raycast Hit:");
            dist = hit.point.z;
            x = hit.point.x;
        }
        else
        {
            dist = 25;
            x = 0;
        }
	}
}
