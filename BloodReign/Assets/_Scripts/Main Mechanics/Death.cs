using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {
    public CameraBehavior behaviour;
    

    private void Start()
    {
        behaviour = GameObject.FindObjectOfType<CameraBehavior>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = Vector3.zero;
        collision.gameObject.GetComponent<WinDetection>().DamagePlayer(100);

        //behaviour.players.Remove(collision.transform);
    }
}
