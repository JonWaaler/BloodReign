using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour {

    public GameObject BulletDeath;



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Spawn System
            GameObject particle1Instance = Instantiate<GameObject>(BulletDeath);

            // Place System
            particle1Instance.transform.position = (collision.transform.position);

            // Set rotation
            particle1Instance.transform.rotation = Quaternion.Euler(0,collision.transform.rotation.eulerAngles.y - 180,0);

            // Delete bullet and set a destroy for system
            collision.gameObject.SetActive(false);
            Destroy(particle1Instance, 1);
        }
    }
}
