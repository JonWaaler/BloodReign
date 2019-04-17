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
    public int pos = 1;
    public int posf = 0;
    private float t_lerp = 0;
    private bool add = true;        // For lerping up to list.count and back to 0
    private bool pause = false;        // For lerping up to list.count and back to 0

    public Material stationary_mat;
    public Material moving_mat;
    bool pulseRumble;
    int pulseCounter = 0;

    public float velocity = 0.005f;
    public float current = 0.0f;
    public float totalTime = 2.0f;
    private void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody>();
        startPos = gameObject.transform;
        GetComponent<MeshRenderer>().material = stationary_mat;
        pos = objectPositions.Count - 1;
        posf = objectPositions.Count - 2;
    }

    void FixedUpdate ()
    {
        // Will have to change to suite Makee Makee, for now key board events
        if (Input.GetKeyDown(activationButton) && start == false)
        {
            start = true;
            add = false;
            pulseRumble = true;
            pulseCounter = 0;
            current = 0;
            totalTime = 1;

            RumblePack[] playersToRumble = FindObjectsOfType<RumblePack>();
            foreach (RumblePack controller in playersToRumble)
            {
                controller.addRumbleTimerH(0.05f, 0.25f);
            }
        }

        if (start)
        {
            // constant lerp stuff
            Vector3 originPos = objectPositions[pos].position;
            Quaternion originRot = objectPositions[pos].rotation;
            Vector3 targetPos = objectPositions[posf].position;
            Quaternion targetRot = objectPositions[posf].rotation;

            float rollLengh = (targetPos - originPos).magnitude; //Distance            

            current += Time.deltaTime;
            float tValue = Mathf.Clamp01(current / totalTime); // figure out how much of % time has passed of elaped time relative to total time 

            //Rumble Stuff
//            if (pulseRumble && pulseCounter % 10 == 0)
//            {
//                RumblePack[] playersToRumble = FindObjectsOfType<RumblePack>();
//                foreach (RumblePack controller in playersToRumble)
//                {
//                    controller.addRumbleTimerH(0.15f, 0.25f);
//                }
//            }
            if (pulseCounter < 10)
            {
                pulseCounter++;
                pulseRumble = false;
            }

            GetComponent<MeshRenderer>().material = moving_mat;
            if (current <= totalTime)
            {
                // update player pos
                gameObject.transform.position = Vector3.Lerp(originPos, targetPos, tValue);
                gameObject.transform.rotation = Quaternion.Lerp(originRot, targetRot, tValue);
            }
            else
            {
                current = 0;
                if (add && pause == false)
                {
                    if (posf < objectPositions.Count - 1)
                    {
                        pos++;
                        posf++;
                    }
                    else
                    {
                        totalTime = 1.0f; // Total time to finish distance at said velocity with: T = D/V
                        start = false;
                        add = true;
                        pos = objectPositions.Count - 1;
                        posf = objectPositions.Count - 2;
                        RumblePack[] playersToRumble = FindObjectsOfType<RumblePack>();
                        foreach (RumblePack controller in playersToRumble)
                        {
                            controller.addRumbleTimerH(0.07f, 0.25f);
                        }
                    }
                }
                else if(add && pause)
                {
                    totalTime = 0.7f; // Total time to finish distance at said velocity with: T = D/V
                    pause = false;
                    pos = 0;
                    posf = 1;
                }
                else
                {
                    if (posf > 1)
                    {
                        pos--;
                        posf--;
                    }
                    else
                    {
                        totalTime = 3.45f;
                        add = true;
                        pause = true;
                        pos = 0;
                        posf = 0;
                    }
                }
                pulseCounter = 0;
                pulseRumble = true;
            }
        }
        else
        {
            gameObject.transform.position = objectPositions[pos].position;
            gameObject.transform.rotation = objectPositions[pos].rotation;
            GetComponent<MeshRenderer>().material = stationary_mat;
        }

    }
}
