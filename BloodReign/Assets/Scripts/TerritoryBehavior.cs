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

    void Start()
    {
        player1_Health = GameObject.Find("Player 1 - Health").GetComponent<Slider>();
        player2_Health = GameObject.Find("Player 2 - Health").GetComponent<Slider>();
        player3_Health = GameObject.Find("Player 3 - Health").GetComponent<Slider>();
        player4_Health = GameObject.Find("Player 4 - Health").GetComponent<Slider>();
    }

    // When player touching collider, give damage
    private void OnCollisionEnter(Collision collision)
    {
        if (terrioryType == TerritoryType.Damage)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.transform.GetChild(1).tag == "Gun")
                {
                    collision.transform.GetChild(1).GetComponent<GunBehavior>().Damage = collision.transform.GetChild(1).GetComponent<GunBehavior>().Damage * 2;
                }
                else
                    print("<color = red>ERROR: Set " + collision.gameObject.name + "'s TAG to Gun./n Or");
            }
        }
    }

    private void OnCollisionStay(Collision collision)
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


    // When player leaves the zone, take away boost
    private void OnCollisionExit(Collision collision)
    {
        if (terrioryType == TerritoryType.Damage)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.transform.GetChild(1).tag == "Gun")
                {
                    collision.transform.GetChild(1).GetComponent<GunBehavior>().Damage = collision.transform.GetChild(1).GetComponent<GunBehavior>().Damage / 2.0f;
                }
                else
                    print("<color = red>ERROR: Set " + collision.gameObject.name + "'s TAG to Gun./n Or");
            }
        }
    }
}