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
        cameraBehavior = FindObjectOfType<CameraBehavior>();
    }

    private void Awake()
    {
        /* use if statements to detect what playerNum this is
         * if(playerNum == 1)
         *      this.material = playerSettings.playerCol1
         * 
         */
        //switch (playerNum)
        //{
        //    case 0:
        //        SkinnedMeshRenderer[] smrs = transform.GetChild(transform.childCount).GetComponentsInChildren<SkinnedMeshRenderer>();
        //        Debug.Log("Children: " + transform.GetChild(transform.childCount).childCount, transform.GetChild(transform.childCount - 1));
        //        if (smrs.Length > 0)
        //            foreach (var smr in smrs)
        //            {

        //                smr.material.color = playerCol[0];

        //                smr.material.color = playerCol[1];

        //                smr.material.color = playerCol[2];

        //                smr.material.color = playerCol[3];


        //            }
        //        break;
        //    case 1:

        //        break;
        //    case 2:

        //        break;
        //    case 3:

        //        break;
        //    default:
        //        break;
        //}

        //transform.GetChild(transform.childCount - 1).GetComponentInChildren<SkinnedMeshRenderer>().material
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
                //slider_PlayerHealth.value = 100;
                GetComponent<Player>().activeState = PlayerState.dead;
            }
            else
            {
                Debug.Log("Player has no lives", gameObject);
                if (playerNum + 1 == 1)
                    GameObject.Find("_GameManager").GetComponent<DialogueManagerWrapper>().p1Dead = true;
                else if (playerNum + 1 == 2)
                    GameObject.Find("_GameManager").GetComponent<DialogueManagerWrapper>().p2Dead = true;
                else if (playerNum + 1 == 3)
                    GameObject.Find("_GameManager").GetComponent<DialogueManagerWrapper>().p3Dead = true;
                else if (playerNum + 1 == 4)
                    GameObject.Find("_GameManager").GetComponent<DialogueManagerWrapper>().p4Dead = true;
                cameraBehavior.players.Remove(transform);
                gameObject.SetActive(false);
                GetComponent<Player>().elementRef.gameObject.SetActive(false);
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
