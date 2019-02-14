// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DialogueManagerWrapper : MonoBehaviour
{
	private float time = 0f;
    public PlayerSettings playerSettings;
	public bool p1Dead = false;
	public bool p2Dead = false;
    public bool p3Dead = false;
    public bool p4Dead = false;
    private bool delay = true;
	public int debugTEMP;

    public GameObject p1Wins, p2Wins, p3Wins, p4Wins;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    void Start()
    {
        if (playerSettings.playerActive_01 == false)
            p1Dead = true;
        if (playerSettings.playerActive_02 == false)
            p2Dead = true;
        if (playerSettings.playerActive_03 == false)
            p3Dead = true;
        if (playerSettings.playerActive_04 == false)
            p4Dead = true;
    }

    void Update()
    {
        int nextEventNum = 0;
        if (p1Dead == false && p2Dead == true && p3Dead == true && p4Dead == true)
            nextEventNum = 3;
        else if (p1Dead == true && p2Dead == false && p3Dead == true && p4Dead == true)
            nextEventNum = 4;
        else if (p1Dead == true && p2Dead == true && p3Dead == false && p4Dead == true)
            nextEventNum = 5;
        else if (p1Dead == true && p2Dead == true && p3Dead == true && p4Dead == false)
            nextEventNum = 6;

        if (nextEventNum == 1)
        {
            // <blank>
        }
        else if (nextEventNum == 2)
        {
            // <blank>
        }
        else if (nextEventNum == 3)
        {
            print("p1Wins");
            GameObject.Find("Canvas_GameUI").transform.Find("Player 1 Wins").gameObject.SetActive(true);
            delay = true;
        }
        if (nextEventNum == 4)
        {
            GameObject.Find("Canvas_GameUI").transform.Find("Player 2 Wins").gameObject.SetActive(true);
            delay = true;
        }
        if (nextEventNum == 5)
        {
            GameObject.Find("Canvas_GameUI").transform.Find("Player 3 Wins").gameObject.SetActive(true);
            delay = true;
        }
        if (nextEventNum == 6)
        {
            GameObject.Find("Canvas_GameUI").transform.Find("Player 4 Wins").gameObject.SetActive(true);
            delay = true;
        }

        if (delay == true)
        {
            time += Time.deltaTime;
            if (time >= 2f)
            {
                p1Wins.SetActive(false);
                p2Wins.SetActive(false);
                p3Wins.SetActive(false);
                p4Wins.SetActive(false);
                delay = false;
                time = 0f;
            }
        }

    }
}
