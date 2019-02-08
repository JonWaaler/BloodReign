using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class WinDetection : MonoBehaviour {
    public float health = 100F;
    //[HideInInspector]
    public int playerNum;
    public Slider slider_PlayerHealth;
    private CameraBehavior cameraBehavior;
    public ParticleSystem Particles_Blood;
    public PlayerSettings playerSettings;
    public GameManager_UI gameManager;
    // Attach to the player.
    private void Start()
    {
        //slider_PlayerHealth = GameObject.Find("Player 1 - Health").GetComponent<Slider>();
        //slider_PlayerHealth = GameObject.Find("Player 2 - Health").GetComponent<Slider>();
        //slider_PlayerHealth = GameObject.Find("Player 3 - Health").GetComponent<Slider>();
        //slider_PlayerHealth = GameObject.Find("Player 4 - Health").GetComponent<Slider>();

        


        cameraBehavior = GameObject.FindObjectOfType<CameraBehavior>();
    }

    public void DamagePlayer(float dmg)
    {
        slider_PlayerHealth.value -= dmg;

        // Spawn blood, set pos to bullet pos
        ParticleSystem bloodInst = Instantiate<ParticleSystem>(Particles_Blood);
        bloodInst.transform.position = transform.position;

        // Wait 35 seconds to destroy the blood
        Destroy(bloodInst, 35);

        if (slider_PlayerHealth.value <= 0.1f)
        {


            if (gameManager.RemoveLife(playerNum))
            {
                print("Removed a life from player");
                slider_PlayerHealth.value = 100;
            }
            else
            {
                Debug.Log("Player has no lives", gameObject);
                cameraBehavior.players.Remove(transform);
                gameObject.SetActive(false);
                playerNum++;
                GameObject.Find("Player" + playerNum + "_Canvas").SetActive(false);
                gameObject.GetComponent<Player>().ability.StopAllCoroutines();
                gameObject.GetComponent<Player>().ability.ResetSphere();
                gameObject.GetComponent<Player>().activeState = PlayerState.dead;
                // wasnt here before !@)(*#)!@#*)#(!#*!)
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        // if this player collides with the bullet
        if (gameObject.GetComponent<Player>().status != StatusEffect.invincible)
        {
            if (other.gameObject.tag == "Bullet")
            {
                // *NOTE
                // You have to keep the player number at the end
                // Name on the player parent
                string temp = gameObject.name;
                temp = temp.Substring(temp.Length - 1);

                if (other.gameObject.GetComponent<Bullet>().ID != temp)
                {
                    DamagePlayer(other.GetComponent<Bullet>().Damage);
                }

            }
        }

    }
}
