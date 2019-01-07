using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {


	void OnCollisionExit(Collision other)
	{
		//if(other.transform.tag == "Player" || other.transform.tag == "MovableObj"){
		//	other.gameObject.GetComponent<Player>().isGrounded = false;
		//}
	}
}
