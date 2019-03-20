using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_Tutorial : MonoBehaviour {

    public bool Toggle_Tutorial;
    public Image Radial_Button;

    public CameraBehavior cameraBehavior;

    private bool[] playerIsReady;
    public List<MeshRenderer> platforms;

    // Use this for initialization
    void Start () {
        playerIsReady = new bool[4];
        playerIsReady[0] = false;
        playerIsReady[1] = false;
        playerIsReady[2] = false;
        playerIsReady[3] = false;
	}
    

	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetKey(KeyCode.B))
        //    Radial_Button.fillAmount += Time.deltaTime;
        //else
        //    Radial_Button.fillAmount -= Time.deltaTime;

        if(Input.GetButton("BButton1") || Input.GetButton("BButton2") || Input.GetButton("BButton3") || Input.GetButton("BButton4"))
        {
            Radial_Button.fillAmount += Time.deltaTime;
        }
        else
        {
            Radial_Button.fillAmount -= Time.deltaTime;
        }

        int readyAmt = 0;
        for (int i = 0; i < cameraBehavior.players.Count; i++)
        {
            if (playerIsReady[i])
            {
                platforms[i].material.color = Color.green;
                readyAmt++;
            }
        }

        if (Input.GetButtonDown("AButton1"))
        {
            playerIsReady[0] = true;
            //readyCount++;
        }
        if (Input.GetButtonDown("AButton2"))
        {
            playerIsReady[1] = true;
            //readyCount++;
        }
        if (Input.GetButtonDown("AButton3"))
        {
            playerIsReady[2] = true;
            //readyCount++;
        }
        if (Input.GetButtonDown("AButton4"))
        {
            playerIsReady[3] = true;
            //readyCount++;
        }

        if(readyAmt == cameraBehavior.players.Count)
        {
            // LoadGame
            SceneManager.LoadScene(2);
        }

        if (Radial_Button.fillAmount > 0.99f)
        {
            // load menu
            SceneManager.LoadScene(0);
        }
    }
}
