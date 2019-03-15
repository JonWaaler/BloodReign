using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowAnimations : MonoBehaviour {

    Animator animator;
    private float PNum;
    private bool hookActive;

	// Use this for initialization
	void Start () {
        animator = this.gameObject.GetComponent<Animator>();
        if (transform.parent.gameObject.name == "Player1_Parent 1")
            PNum = 1;
        else if (transform.parent.gameObject.name == "Player2_Parent 2")
            PNum = 2;
        else if (transform.parent.gameObject.name == "Player3_Parent 3")
            PNum = 3;
        else if (transform.parent.gameObject.name == "Player4_Parent 4")
            PNum = 4;
    }
	
	// Update is called once per frame
	void Update () {
        hookActive = transform.parent.GetComponent<HookAbility>().hookActive;
        animator.SetFloat("InputX", Input.GetAxis("V_LStick" + PNum));
        animator.SetFloat("InputZ", Input.GetAxis("H_LStick" + PNum));

        if (Input.GetButtonDown("LB" + PNum) && hookActive)
        {
            //switch animation
            animator.SetBool("IsGrapple", true);
        }
        else
            animator.SetBool("IsGrapple", false);
    }
}
