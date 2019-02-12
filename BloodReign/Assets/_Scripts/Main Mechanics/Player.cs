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

    [Header("Color")]
    public List<Color> playerColors; // 4 colours for now

    [Header("Particle Effects Death")]
    public GameObject system;
    public GameObject system1;
    // Particle system
    public GameObject p_Inst;
    public GameObject p_Inst1;

    [Header("Element Ref")]
    public Transform elementRef;
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


        // Respawn particle systems
        p_Inst = Instantiate(system);
        p_Inst1 = Instantiate(system1);
        p_Inst.SetActive(false);
        p_Inst1.SetActive(false);
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
        //if (activeState.Equals(PlayerState.dead))
        //    return;

        xVel = Input.GetAxis(H_LS_PNum);
        zvel = Input.GetAxis(V_LS_PNum);
                
        inputVector = new Vector3(xVel, 0, zvel);
        
        // Player movement
        if(activeState == PlayerState.alive)
        {
            if (xVel != 0 || zvel != 0)
            {
                rb.AddForce(inputVector.normalized * speed);
            }
            rb.velocity = new Vector3(speed * xVel, rb.velocity.y-1.5f, speed * zvel);


            if (Input.GetButtonDown(abilButton) && Time.time > nextAbil && !status.Equals(StatusEffect.grappled))
            {
                // set time for when next use of ability available
                nextAbil = Time.time + ability.abilCool;
                ability.AbilityExcecution();
            }
            // Look direction
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw(H_RS_PNum) + Vector3.forward * -Input.GetAxisRaw(V_RS_PNum);
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
            p_Inst.SetActive(false);
            p_Inst1.SetActive(false);
        }

        if (activeState == PlayerState.dead)
        {
            //print("Dead");
            elementRef.gameObject.SetActive(false);

            // Collision Off
            GetComponent<CapsuleCollider>().enabled = false;

            // Movement
            if (xVel != 0 || zvel != 0)
            {
                rb.AddForce(inputVector.normalized * speed/2f);
            }
            rb.velocity = new Vector3(speed * xVel, rb.velocity.y, speed/2f * zvel);

            // Set Hight
            rb.transform.position = new Vector3(rb.transform.position.x, 2, rb.transform.position.z);

            // Look direction
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw(H_RS_PNum) + Vector3.forward * -Input.GetAxisRaw(V_RS_PNum);
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }

            p_Inst.SetActive(true);
            p_Inst1.SetActive(true);
            p_Inst.transform.position = new Vector3(transform.position.x, 0.69f, transform.position.z);
            p_Inst1.transform.position = new Vector3(transform.position.x, 1.02f, transform.position.z);

            if (GetComponent<WinDetection>().slider_PlayerHealth.value < 100)
                GetComponent<WinDetection>().slider_PlayerHealth.value += Time.deltaTime * 50f;

            // The Respawn button
            if (Input.GetButtonDown(AButton_PNum) && GetComponent<WinDetection>().slider_PlayerHealth.value >= 100)
            {
                rb.transform.position = new Vector3(rb.transform.position.x, 1, rb.transform.position.z);
                GetComponent<CapsuleCollider>().enabled = true;
                elementRef.gameObject.SetActive(true);
                GetComponent<WinDetection>().slider_PlayerHealth.value = 100;
                activeState = PlayerState.alive;
            }
        }
    }

    private void FixedUpdate()
    {
        /* ---- How the element stuff works ----
         * "GunSettings" where the game sets itself up
         * instances an element based off of the gun type.
         * It then sets this scripts "elementRef" to what it spawned
         * Then we controll the position here to fix the stutter bug.
         */
        elementRef.position = transform.position;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "MovableObj")
		{
			transform.SetParent(other.transform);
        }
		else
		{
            //transform.localScale = new Vector3(1, 1, 1);
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