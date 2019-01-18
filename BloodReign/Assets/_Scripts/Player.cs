using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAbil
{
    roll,
    invisible,
    teleport,
    hook
}
public enum PlayerState
{
    alive,
    dead
}
public enum StatusEffect
{
    nothing,
    immobile,
    grappled,
    invincible
}

public class Player : MonoBehaviour {
	private float xVel;
	private float zvel;
	private Vector3 inputVector;
	public bool isGrounded;
	public float speed;
	public string H_LS_PNum, V_LS_PNum, H_RS_PNum, V_RS_PNum, AButton_PNum;
	public float fallMultiplier = 2.5f;
	public float lowJumpMultiplier = 2f;
	public PlayerSettings playerSettings;
    public string abilButton;
    protected float nextAbil; // deltatime until next ability use
    public PlayerAbil playerEnum;
    public PlayerState activeState;
    public StatusEffect status;
    [Range(1, 10)]
	public float jumpVelocity;
    Ray groundedRay;
    public Rigidbody rb;
    public AbilityCommand ability;
	void Awake ()
	{
		Rigidbody rb = GetComponent<Rigidbody>();

		if (playerSettings.playerActive_01 == false && gameObject.name == "Player1_Parent 1")   
		{
			//player not active
		}
		if (playerSettings.playerActive_02 == false && gameObject.name == "Player2_Parent 2")
		{
			//player not active
		}
		if (playerSettings.playerActive_03 == false && gameObject.name == "Player3_Parent 3")
		{
			//player not active
		}
		if (playerSettings.playerActive_04 == false && gameObject.name == "Player4_Parent 4")
		{
			//player not active
		}

        switchPlayer(playerEnum);
        activeState = PlayerState.alive;
        status = StatusEffect.nothing;
    }
	// Change ability // useless atm
    public void switchPlayer(PlayerAbil newAbli)
    {
        switch (playerEnum)
        {
            case PlayerAbil.roll:
                ability = GetComponent<RollAbility>();
                break;
            case PlayerAbil.invisible:
                ability = GetComponent<InvisAbility>();
                break;
            case PlayerAbil.teleport:
                ability = GetComponent<TeleportAbility>();
                break;
            case PlayerAbil.hook:
                ability = GetComponent<HookAbility>();
                break;                
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState.Equals(PlayerState.dead))
            return;

        xVel = Input.GetAxis(H_LS_PNum);
        zvel = Input.GetAxis(V_LS_PNum);

        inputVector = new Vector3(xVel, 0, zvel);

        if (xVel != 0 || zvel != 0)
        {
            rb.AddForce(inputVector.normalized * speed);
        }

        rb.velocity = new Vector3(speed * xVel, rb.velocity.y, speed * zvel);

        Vector3 playerDirection = Vector3.right * Input.GetAxisRaw(H_RS_PNum) + Vector3.forward * -Input.GetAxisRaw(V_RS_PNum);
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            //playerDirection.z += 45;
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            //transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        }

        // isGrounded detection
        groundedRay = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(groundedRay, out hit, .75f))
        {


            Debug.DrawRay(groundedRay.origin, groundedRay.direction * 2);
            if (hit.transform.tag == "Floor" || hit.transform.tag == "MovableObj")
                isGrounded = true;
        }
        else
            isGrounded = false;

        if (Input.GetButtonDown(AButton_PNum) && isGrounded)
        {
            rb.velocity = new Vector3(0, jumpVelocity, 0);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton(AButton_PNum))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (Input.GetButtonDown(abilButton) && Time.time > nextAbil && !status.Equals(StatusEffect.grappled))
        {
            // set time for when next use of ability available
            nextAbil = Time.time + ability.abilCool;
            ability.AbilityExcecution();
        }
    }
	
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "MovableObj")
		{
			transform.SetParent(other.transform);
        }
		else
		{
			transform.SetParent(null);
		}
    }

	void OnCollisionStay(Collision other)
	{
		//if (other.gameObject.tag == "Floor" || other.gameObject.tag == "MovableObj")
		//{
		//	isGrounded = true;
		//}
	}

	// void OnCollisioneExit(Collision other)
	// {
	// 	if (other.gameObject.tag == "Floor")
	// 	{
	// 		isGrounded = false;
	// 		Debug.Log("false");
	// 	}
	// }
}