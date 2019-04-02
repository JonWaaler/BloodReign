using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Movement : MonoBehaviour {
    
    /* attach to moveable object
     * set tran
     * 
     */

    public KeyCode activationButton;
    public float Speed = .1f;
    public bool finishAtStartPos = false;
    public List<Transform> objectPositions;
    //public IEnumerable movementMode;      // For later use to implement a different lerp method

    private Transform startPos;
    private bool start = false;
    public int pos = 0;
    private float t_lerp = 0;
    private bool add = true;        // For lerping up to list.count and back to 0

    public Material stationary_mat;
    public Material moving_mat;
    bool pulseRumble;
    int pulseCounter = 0;

    private void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        startPos = gameObject.transform;
        GetComponent<MeshRenderer>().material = stationary_mat;
    }

    void FixedUpdate ()
    {
        // Will have to change to suite Makee Makee, for now key board events
        if (Input.GetKeyDown(activationButton) && start == false)
        {
            start = true;
            pulseRumble = true;
            pulseCounter = 0;
        }

        if (start)
        {
            if (pulseRumble && pulseCounter % 10 == 0)
            {
                RumblePack[] playersToRumble = FindObjectsOfType<RumblePack>();
                foreach (RumblePack controller in playersToRumble)
                {
                    controller.addRumbleTimerH(0.3f, 0.25f);
                }
            }

            if (pulseCounter < 30)
            {
                pulseCounter++;
                pulseRumble = false;
            }

            GetComponent<MeshRenderer>().material = moving_mat;

            // update player pos
            gameObject.transform.position = Vector3.Lerp(transform.position, objectPositions[pos].position, t_lerp);
            gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, objectPositions[pos].rotation, t_lerp);
            // timer
            t_lerp += Time.deltaTime * Speed;

            // Check which dir we need to go
            if (pos == objectPositions.Count)
                add = false;


            // if at end change pos num
            if (t_lerp > 1)     // Asks what we want to change pos to
            {
                if (add)
                {
                    t_lerp = 0;
                    pos++;
                }
                else
                {
                    t_lerp = 0;
                    pos--;
                }
                pulseCounter = 0;
                pulseRumble = true;
            }

            if (pos == objectPositions.Count)
            {
                start = false;
                GetComponent<MeshRenderer>().material = stationary_mat;

                pos = 0;
            }
        }
        else
        {
            gameObject.transform.position = startPos.position;
            gameObject.transform.rotation = startPos.rotation;
        }

    }
}
