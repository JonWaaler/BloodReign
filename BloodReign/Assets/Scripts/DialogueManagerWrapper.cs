// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class DialogueManagerWrapper : MonoBehaviour
{
	const string DLL_NAME = "DialogueManagerPlugin";
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

    // [DllImport(DLL_NAME)]
    // private static extern void startTXT();
    // [DllImport(DLL_NAME)]
    // private static extern void endTXT();
    [DllImport(DLL_NAME)]
	private static extern void p1WinTXT();
	[DllImport(DLL_NAME)]
	private static extern void p2WinTXT();
    [DllImport(DLL_NAME)]
    private static extern void p3WinTXT();
    [DllImport(DLL_NAME)]
    private static extern void p4WinTXT();
    [DllImport(DLL_NAME)]
	private static extern int getNextEvent();

    void Start()
    {
        //print("Player 1/2 Wins should be already false...");
        //GameObject.Find("Player 1 Wins").SetActive(false);
        //GameObject.Find("Player 2 Wins").SetActive(false);

    }

    void Update()
    {
        // if game end
        // endTXT();

        if (player1 != null)
        {
            if (player1.activeInHierarchy && player2 == null && player3 == null && player4 == null)
                p1WinTXT();
        }
        if (player2 != null)
        {
            if (player2.activeInHierarchy && player1 == null && player3 == null && player4 == null)
                p2WinTXT();
        }
        if (player3 != null)
        {
            if (player3.activeInHierarchy && player2 == null && player1 == null && player4 == null)
                p3WinTXT();
        }
        if (player4 != null)
        {
            if (player4.activeInHierarchy && player2 == null && player3 == null && player1 == null)
                p4WinTXT();
        }

        int nextEventNum = getNextEvent();

        if (nextEventNum == 1)
        {

        }
        else if (nextEventNum == 2)
        {

        }
        else if (nextEventNum == 3)
        {
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
            print("fukc you");
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
