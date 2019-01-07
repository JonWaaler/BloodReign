using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinDetection : MonoBehaviour {
    public float health = 100F;
    public Slider slider_PlayerHealth;
    private CameraBehavior cameraBehavior;
    public ParticleSystem Particles_Blood;
    public PlayerSettings playerSettings;
    public bool isInvincible = false;
    public bool isGrappled = false;
    // Attach to the player.
    private void Start()
    {
        if (gameObject.name == "Player1_Parent 1")
        {
            slider_PlayerHealth = GameObject.Find("Player 1 - Health").GetComponent<Slider>();
        }
        else if (gameObject.name == "Player2_Parent 2")
        {
            slider_PlayerHealth = GameObject.Find("Player 2 - Health").GetComponent<Slider>();
        }
        else if (gameObject.name == "Player3_Parent 3")
        {
            slider_PlayerHealth = GameObject.Find("Player 3 - Health").GetComponent<Slider>();
        }
        else if (gameObject.name == "Player4_Parent 4")
        {
            slider_PlayerHealth = GameObject.Find("Player 4 - Health").GetComponent<Slider>();
        }
        cameraBehavior = GameObject.FindObjectOfType<CameraBehavior>();
    }

    void OnDestroy()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Col: " + other.tag, other.gameObject);

        // if this player collides with the bullet
        if (!isInvincible)
        {
            if (other.gameObject.tag == "Bullet" && isInvincible == false)
            {
                // *NOTE
                // You have to keep the player number at the end
                // Name on the player parent
                string temp = gameObject.name;
                temp = temp.Substring(temp.Length - 1);

                if (other.gameObject.GetComponent<Bullet>().ID != temp)
                {
                    slider_PlayerHealth.value -= other.GetComponent<Bullet>().Damage;
                    print("<color=green>Player " + other.GetComponent<Bullet>().ID + " did " + other.GetComponent<Bullet>().Damage + " Damage</color>");

                    // Spawn blood, set pos to bullet pos
                    ParticleSystem bloodInst = Instantiate<ParticleSystem>(Particles_Blood);
                    bloodInst.transform.position = other.transform.position;

                    // Debug Direction
                    //Debug.DrawRay(bloodInst.transform.position, other.transform.position * 5, Color.yellow);

                    //bloodInst.transform.rotation = other.transform.rotation;
                    Destroy(bloodInst, 35);

                    //print("Made it here");
                    if (slider_PlayerHealth.value <= 0.1f) //*was 0.2*
                    {
                        //if (gameObject.name == "Player Parent 1")
                        //{
                        //    GameObject.Find("_GameManager").GetComponent<DialogueManagerWrapper>().p2Win = true;
                        //}
                        //else if (gameObject.name == "Player Parent 2")
                        //{
                        //    GameObject.Find("_GameManager").GetComponent<DialogueManagerWrapper>().p1Win = true;
                        //}

                        cameraBehavior.players.Remove(transform);
                        if(gameObject.GetComponent<Ability3>().enabled )
                        {
                            gameObject.GetComponent<Ability3>().StopAllCoroutines();
                            gameObject.GetComponent<Ability3>().resetSphere();
                        }
                        else if (gameObject.GetComponent<Ability4>().enabled)
                        {
                            gameObject.GetComponent<Ability4>().StopAllCoroutines();
                            gameObject.GetComponent<Ability4>().resetSphere();
                        }                    
                        Destroy(gameObject);
                    }
                }

            }
        }

    }
}
