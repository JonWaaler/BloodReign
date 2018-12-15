using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability4 : AbilityCommand
{
    Ability4()
    {
        abilCool = 1.0f;
        lerpSpd = 13.0f;
        abilLength = 8.0f;
    }
    public float hookReelSpd = 0.25f;
    private bool extendHook = false;
    private bool reelHook = false;
    public GameObject collisionObj;
    GameObject sphereCol;

    private enum grabbedObj
    {
        nothing,
        wall,
        player,
        other
    };
    void Start()
    {
        sphereCol = Instantiate(collisionObj);
        sphereCol.name = "grappleHook";
        sphereCol.transform.position = transform.position;
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
        sphereCol.GetComponent<SphereCollisionCheck>().playerThrow = transform.gameObject;
        extendHook = false;
        reelHook = false;
    }
    public void resetSphere()
    {
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
    }
    public override void AbilityExcecution()
    {
        activate();
    }
    private void activate()
    {
//        if (Input.GetButtonDown(abilButton) && Time.time > nextAbil && extendHook == false && reelHook == false)
        {
            // set time for when next use of ability available
 //           nextAbil = Time.time + abilCool;
            sphereCol.transform.position = transform.position + (transform.forward * 1.0f);
            StartCoroutine(HookReelOut(transform.position, lerpSpd, abilLength));
        }
    }
    private IEnumerator HookReelOut(Vector3 origin, float moveSpeed, float range)
    {
        // Make sure data is clean before we start
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = true;
        Vector3 initalFoward = transform.forward;
        float current = 0.0f;
        sphereCol.GetComponent<Collider>().enabled = true;
        sphereCol.GetComponent<SphereCollisionCheck>().isCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision = false;
        extendHook = true;
        grabbedObj grabbed = grabbedObj.nothing;
        // extend hook out
        while (extendHook == true)
        {
            transform.position = origin;
            // Continue Hook until any 3 conditions met
            if (sphereCol.GetComponent<SphereCollisionCheck>().isCollision)
            { // Wall hit
                float spaceBetweenWall = (sphereCol.transform.position - transform.position).magnitude;
                if (spaceBetweenWall < 1.0)
                    grabbed = grabbedObj.nothing;
                else
                {
                    extendHook = false;
                    grabbed = grabbedObj.wall;
                }
            }
            else if (sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision)
            { // Player hit
                extendHook = false;
                grabbed = grabbedObj.player;
                sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<WinDetection>().isGrappled = true;
            }
            else if (current > range) // Missed
            {
                extendHook = false;
                grabbed = grabbedObj.nothing;
            }
            else
            {
                grabbed = grabbedObj.other;
                current = current + Time.deltaTime * moveSpeed;
                sphereCol.transform.position += (initalFoward * moveSpeed * Time.deltaTime);
                nextAbil += Time.deltaTime / 2; // Don't reset cool down if extending
            }
            yield return null;
        }
        reelHook = true;
        // reel back depending on grabbed obj or max length
        if (grabbed == grabbedObj.wall)
        {
            StartCoroutine(lerpHook(transform.position, sphereCol.transform.position, hookReelSpd * 2, transform.gameObject));
            while (reelHook)
            {
                yield return null;
            }
        }
        else if (grabbed == grabbedObj.player)
        {
            StartCoroutine(lerpHook(sphereCol.GetComponent<SphereCollisionCheck>().playerHit.transform.position, transform.position + (initalFoward * 1.5f), hookReelSpd * 2, sphereCol.GetComponent<SphereCollisionCheck>().playerHit));
            StartCoroutine(lerpHook(sphereCol.transform.position, transform.position, hookReelSpd * 2, sphereCol));
            while (reelHook)
            {
                transform.position = origin;
                yield return null;
            }
        }
        else if (grabbed == grabbedObj.nothing)
        {
            //transform.position = origin;
            StartCoroutine(lerpHook(sphereCol.transform.position, transform.position, hookReelSpd * 3, sphereCol.transform.gameObject));
            while (reelHook)
                yield return null;
        }
        // Make sure its set off after were done
        sphereCol.GetComponent<SphereCollisionCheck>().isCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision = false;

        // Don't need the collider anymore
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
        if (grabbed == grabbedObj.player)
            sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<WinDetection>().isGrappled = false;

    }
    private IEnumerator lerpHook(Vector3 start, Vector3 end, float velocity, GameObject affectedObj)
    {
        float current = 0.0f;//Elapsed time
        float rollLengh = (end - start).magnitude; //Distance
        float totalTime = rollLengh / velocity; // Total time to finish distance at said velocity with: T = D/V

        while (current <= totalTime)
        {
            current = current + Time.deltaTime;
            float tValue = Mathf.Clamp01(current / totalTime);
            affectedObj.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(start, end, tValue));
            yield return null;
        }
        reelHook = false;
    }
}
