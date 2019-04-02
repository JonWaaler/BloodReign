using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyManager : MonoBehaviour {

    public GameObject prefab_dummy;
    public List<Transform> dummy_position;

    private CameraBehavior cameraBehavior;

	// Use this for initialization
	void Start () {
        cameraBehavior = FindObjectOfType<CameraBehavior>();
        for (int i = 0; i < 4; i++)
        {
            GameObject instDummy = Instantiate(prefab_dummy);
            instDummy.transform.position = dummy_position[i].position;
            cameraBehavior.players.Add(instDummy.transform);
            // This allows the dummy damage to set its health bar to the dummy pos
            //instDummy.GetComponent<DummyDamage>().dummy = cameraBehavior.players[i];
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
