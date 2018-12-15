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
        behaviour.players.Remove(collision.transform);
        Destroy(collision.gameObject);
    }
}
