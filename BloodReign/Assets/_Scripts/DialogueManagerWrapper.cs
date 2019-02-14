// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DialogueManagerWrapper : MonoBehaviour
{
	private float time = 0f;
	public bool p1Win = false;
	public bool p2Win = false;
    public bool p3Win = false;
    public bool p4Win = false;
    private bool delay = true;
	public int debugTEMP;

    public GameObject p1Wins, p2Wins, p3Wins, p4Wins;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    void Update()
    {
        int nextEventNum = 0;
        // Change how death detecion works because players never go null
        if (player1 != null)
        {
            if (player1.activeInHierarchy && player2 == null && player3 == null && player4 == null)
                nextEventNum = 3;
        }
        if (player2 != null)
        {
            if (player2.activeInHierarchy && player1 == null && player3 == null && player4 == null)
            {
                nextEventNum = 4;
                print("test");
            }
        }
        if (player3 != null)
        {
            if (player3.activeInHierarchy && player2 == null && player1 == null && player4 == null)
                nextEventNum = 5;
        }
        if (player4 != null)
        {
            if (player4.activeInHierarchy && player2 == null && player3 == null && player1 == null)
                nextEventNum = 6;
        }

        if (nextEventNum == 1)
        {

        }
        else if (nextEventNum == 2)
        {

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
