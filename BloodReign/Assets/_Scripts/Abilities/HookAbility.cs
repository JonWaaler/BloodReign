using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAbility : AbilityCommand
{
    public HookAbility()
    {
        if (abilSettings != null)
            abilCool = abilSettings.abilCool_3;
    }
    private bool extendHook = false;
    private bool reelHook = false;
    private GameObject sphereCol;
    public float grappleDmg;

    private enum grabbedObj
    {
        nothing,
        wall,
        player,
        dummy,
        other
    };
    void Start()
    {
        sphereCol = Instantiate(abilSettings.collisionSphereInit_2);
        sphereCol.name = "grappleHook_" + transform.name.ToString();
        sphereCol.transform.position = transform.position;
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
        sphereCol.GetComponent<SphereCollisionCheck>().playerThrow = transform.gameObject;
        extendHook = false;
        reelHook = false;
    }
    public override void ResetSphere()
    {
        if (sphereCol.GetComponent<SphereCollisionCheck>().playerHit && sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision == true)
        {
            sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<Player>().status = StatusEffect.nothing;
        }
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isDummyCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().playerThrow = transform.gameObject;
        extendHook = false;
        reelHook = false;
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
            sphereCol.transform.position = transform.position;
            StartCoroutine(HookReelOut(transform.position, abilSettings.lerpSpd_3, abilSettings.abilLength_3));
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
        sphereCol.GetComponent<SphereCollisionCheck>().isDummyCollision = false;
        extendHook = true;
        grabbedObj grabbed = grabbedObj.nothing;
        // extend hook out
        while (extendHook == true)
        {
            if (transform.GetComponent<Player>().status != StatusEffect.grappled)
                transform.position = origin;

            // Continue Hook until any 3 conditions met
            if (sphereCol.GetComponent<SphereCollisionCheck>().isCollision)
            { // Wall hit
                float spaceBetweenWall = (sphereCol.transform.position - transform.position).magnitude;
                grabbed = grabbedObj.wall;
                extendHook = false;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.0f, layerMask))
                {
                    if (hit.collider.tag.Equals("Wall"))
                    {
                        if (Mathf.Abs(hit.distance) < 2.0f)
                        {
                            grabbed = grabbedObj.nothing;
                        }
                    }
                }
            }
            else if (sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision)
            { // Player hit
                extendHook = false;
                sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<WinDetection>().DamagePlayer(grappleDmg);
                if (sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<Player>().activeState == PlayerState.alive)
                {
                    if (sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<Player>().status == StatusEffect.grappled)
                    {
                        grabbed = grabbedObj.wall;
                    }
                    else
                    {
                        grabbed = grabbedObj.player;
                        sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<Player>().status = StatusEffect.grappled;
                    }
                }
                else
                {
                    extendHook = false;
                    grabbed = grabbedObj.nothing;
                }
            }
            else if (sphereCol.GetComponent<SphereCollisionCheck>().isDummyCollision)
            { // Dummy hit
                extendHook = false;
                grabbed = grabbedObj.dummy;
                sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<DummyDamage>().health.value -= abilSettings.grappleDmg;
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
            }
            yield return null;
        }
        reelHook = true;
        // reel back depending on grabbed obj or max length
        if (grabbed == grabbedObj.wall)
        {
            StartCoroutine(lerpHook(gameObject, sphereCol, abilSettings.lerpReelSpd * 2, transform.gameObject));
            while (reelHook)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2.0f, layerMask))
                {
                    if (hit.collider.tag.Equals("Wall"))
                    {
                        if (Mathf.Abs(hit.distance) < 2.0f)
                        {
                            StopCoroutine(lerpHook(gameObject, sphereCol, abilSettings.lerpReelSpd * 2, transform.gameObject));
                            reelHook = false;
                        }
                    }
                }
                yield return null;
            }
        }
        else if (grabbed == grabbedObj.player)
        {
            StartCoroutine(lerpHook(sphereCol.GetComponent<SphereCollisionCheck>().playerHit, gameObject, abilSettings.lerpReelSpd * 2, sphereCol.GetComponent<SphereCollisionCheck>().playerHit));
            StartCoroutine(lerpHook(sphereCol, gameObject, abilSettings.lerpReelSpd * 2, sphereCol));
            while (reelHook)
            {
                if (transform.GetComponent<Player>().status != StatusEffect.grappled)
                {
                    transform.position = origin;
                }
                yield return null;
            }
            sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<Player>().status = StatusEffect.nothing;
        }
        else if (grabbed == grabbedObj.dummy)
        {
            StartCoroutine(lerpHook(sphereCol.GetComponent<SphereCollisionCheck>().playerHit, gameObject, abilSettings.lerpReelSpd * 3, sphereCol.GetComponent<SphereCollisionCheck>().playerHit));
            StartCoroutine(lerpHook(sphereCol, gameObject, abilSettings.lerpReelSpd * 2, sphereCol));
            while (reelHook)
            {
                yield return null;
            }
        }
        else if (grabbed == grabbedObj.nothing)
        {
            //transform.position = origin;
            StartCoroutine(lerpHook(sphereCol, gameObject, abilSettings.lerpReelSpd * 3, sphereCol.transform.gameObject));
            while (reelHook)
                yield return null;
        }



        if (grabbed == grabbedObj.player)
            sphereCol.GetComponent<SphereCollisionCheck>().playerHit.GetComponent<Player>().status = StatusEffect.nothing;

        // Make sure its set off after were done
        sphereCol.GetComponent<SphereCollisionCheck>().isCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isPlayerCollision = false;
        sphereCol.GetComponent<SphereCollisionCheck>().isDummyCollision = false;

        // Don't need the collider anymore
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;

    }
    private IEnumerator lerpHook(GameObject start, GameObject end, float velocity, GameObject affectedObj)
    {
        float current = 0.0f;//Elapsed time
        float rollLengh = (end.transform.position - start.transform.position).magnitude; //Distance
        float totalTime = rollLengh / velocity; // Total time to finish distance at said velocity with: T = D/V

        while (current <= totalTime)
        {
            if ((end.transform.position - start.transform.position).magnitude < .025f)
            {
                current = totalTime;
            }
            current = current + Time.deltaTime;
            float tValue = Mathf.Clamp01(current / totalTime);
            affectedObj.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(start.transform.position, end.transform.position, tValue));
            yield return null;
        }
        reelHook = false;
    }
}
