using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : AbilityCommand
{
    public TeleportAbility()
    {
        abilCool = abilSettings.abilCool_2;
    }
    private GameObject sphereCol;
    public override void AbilityExcecution()
    {
        activate();
    }
    void Start()
    {
        sphereCol = Instantiate(abilSettings.collisionSphereInit_1);
        sphereCol.transform.position = transform.position;
        sphereCol.name = "teleporter" + transform.name.ToString();
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
        sphereCol.GetComponent<SphereCollisionCheck>().playerThrow = transform.gameObject;
    }
    public override void ResetSphere()
    {
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
    }

    private void activate()
    {
        //   if (Input.GetButtonDown(abilButton) && Time.time > nextAbil)
        {
            // set time for when next use of ability available
            //     nextAbil = Time.time + abilCool;
            StartCoroutine(Teleport(transform.position, abilSettings.lerpSpd_2, abilSettings.abilLength_2));
        }
    }
    // NOTE: Need to turn off sphereCol if player dies
    private IEnumerator Teleport(Vector3 origin, float velocity, float maxDistance)
    {
        sphereCol.transform.position = origin;
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = true;
        sphereCol.GetComponent<Collider>().enabled = true;
        Vector3 initalFoward = transform.forward;
        float deltaX = 0;
        while (deltaX <= maxDistance)
        {
            deltaX += velocity * Time.deltaTime;
            sphereCol.transform.position += (initalFoward * velocity * Time.deltaTime);
            yield return null;
        }
        transform.position = sphereCol.transform.position;
        sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
    }
}