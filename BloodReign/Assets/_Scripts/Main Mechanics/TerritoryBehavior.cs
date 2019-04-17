using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryBehavior : MonoBehaviour {

    public enum TerritoryType { Damage, Regen };
    public TerritoryType terrioryType;
    private Slider player1_Health;
    private Slider player2_Health;
    private Slider player3_Health;
    private Slider player4_Health;

    public Texture doubleDmgActive_EmissionMap;
    public Texture EmissionMap;

    void Start()
    {
        GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", EmissionMap);

        player1_Health = GameObject.Find("Player 1 - Health").GetComponent<Slider>();
        player2_Health = GameObject.Find("Player 2 - Health").GetComponent<Slider>();
        player3_Health = GameObject.Find("Player 3 - Health").GetComponent<Slider>();
        player4_Health = GameObject.Find("Player 4 - Health").GetComponent<Slider>();
    }

    // When player touching collider, give damage
    private void OnTriggerEnter(Collider collision)
    {

        if (terrioryType == TerritoryType.Damage)
        {
            if (collision.tag == "Player")
            {
                GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", doubleDmgActive_EmissionMap);
            }
        }
        if (terrioryType == TerritoryType.Damage)
        {
            if (collision.tag == "Player")
            {
                GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", doubleDmgActive_EmissionMap);
                collision.GetComponent<RumblePack>().addRumbleTimerL(0.7f, 0.02f);
                collision.GetComponent<RumblePack>().addRumbleTimerL(0.3f, 0.1f);
            }
        }
    }


    private void OnTriggerStay(Collider collision)
    {


        if(terrioryType == TerritoryType.Regen)
        {
            if (collision.gameObject.name == "Player Parent 1")
                player1_Health.value += Time.deltaTime;

            if (collision.gameObject.name == "Player Parent 2")
                player2_Health.value += Time.deltaTime;

            if (collision.gameObject.name == "Player Parent 3")
                player3_Health.value += Time.deltaTime;

            if (collision.gameObject.name == "Player Parent 4")
                player4_Health.value += Time.deltaTime;

        }

    }


    // When player leaves the zone, take away boost
    private void OnTriggerExit(Collider collision)
    {

        if (terrioryType == TerritoryType.Damage)
        {
            if (collision.tag == "Player")
            {
                GetComponent<MeshRenderer>().material.SetTexture("_EmissionMap", EmissionMap);

            }
        }
    }
}