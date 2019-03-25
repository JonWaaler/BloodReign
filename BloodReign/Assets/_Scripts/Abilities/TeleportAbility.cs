using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : AbilityCommand
{
    public SoundManager soundManager;
    [SerializeField]
    private GameObject telePartIns = null;

    public TeleportAbility()
    {
        if (abilSettings != null)
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
        soundManager.Play(Sounds.SoundName.Teleport);
        StartCoroutine(Teleport(transform.position, abilSettings.lerpSpd_2, abilSettings.abilLength_2));
    }
    private IEnumerator Teleport(Vector3 origin, float velocity, float maxDistance)
    {
        sphereCol.transform.position = origin;
        //sphereCol.GetComponentInChildren<MeshRenderer>().enabled = true;
        sphereCol.GetComponent<Collider>().enabled = true;
        Vector3 initalFoward = transform.forward;
        float deltaX = 0;

        // Spawn Particle System
        if (telePartIns == null)
            telePartIns = Instantiate(abilSettings.telePart);
        telePartIns.transform.SetParent(transform);
        while (deltaX <= maxDistance)
        {
            // Place System
            telePartIns.transform.position = transform.position;

            deltaX += velocity * Time.deltaTime;
            sphereCol.transform.position += (initalFoward * velocity * Time.deltaTime);
            yield return null;
        }
        // Set a destroy for system
        Destroy(telePartIns, 1);
        transform.position = sphereCol.transform.position;
     //   sphereCol.GetComponentInChildren<MeshRenderer>().enabled = false;
        sphereCol.GetComponent<Collider>().enabled = false;
    }
}