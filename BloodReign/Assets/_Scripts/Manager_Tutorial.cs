using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_Tutorial : MonoBehaviour {

    public bool Toggle_Tutorial;
    public Image Radial_Button;

    public CameraBehavior cameraBehavior;

    // Player stuff
    public List<RumblePack> readyPlayers;
    public List<bool> readyBools;
    public List<GameObject> players;

    public List<MeshRenderer> platforms;
    public int readyAmt = 0;
    public int playersActive = 4; // Will subtract 1 for every non active player

    // Use this for initialization
    void Start () {
        
        playersActive = cameraBehavior.players.Count - 4;
        print("CamBeCount: " + cameraBehavior.players.Count);

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                readyPlayers.Add(players[i].GetComponent<RumblePack>());
                readyBools.Add(false);
            }
        }
    }
    

	// Update is called once per frame
	void Update ()
    {
        // Back button stuff
        bool goBack = false;
        for(int i = 0; i < readyPlayers.Count; i++)
        {
            if ((int)readyPlayers[i].state.Buttons.B == 0 && readyPlayers[i].state.IsConnected)
            {
                goBack = true;
                i = readyPlayers.Count;
            }
        }

        // Controls radial button visuals
        // Very bottom, check Radial_Button.fillAmount value and load scene if >= .99
        if (goBack)
        {
            Radial_Button.fillAmount += Time.deltaTime/2.0f;
        }
        else
        {
            Radial_Button.fillAmount -= Time.deltaTime;
        }

        for(int i = readyPlayers.Count-1; i >= 0; i--)
        {
            if(players[i] == null)
            {
                players.Remove(players[i]);
                readyPlayers.Remove(readyPlayers[i]);
                readyBools.Remove(readyBools[i]);
            }
        }


        // Ready up
        for (int i = 0; i < readyPlayers.Count; i++)
        {
            if (readyPlayers[i].getButtonDown(0) && readyBools[i] == false)
            {
                readyBools[i] = true;
                platforms[(int)readyPlayers[i].playerIndex].material.color = Color.green;
                readyAmt++;
            }
            //if (readyBools[i])
        }

        if (Input.GetKeyDown(KeyCode.Keypad1) && readyBools[0] == false)
        {
            if (players[0].activeSelf)
            {
                readyBools[0] = true;
                platforms[0].material.color = Color.green;
                readyAmt++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad2) && readyBools[1] == false)
        {
            if (players[1].activeSelf)
            {
                readyBools[1] = true;
                platforms[1].material.color = Color.green;
                readyAmt++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad3) && readyBools[2] == false)
        {
            if (players[2].activeSelf)
            {
                readyBools[2] = true;
                platforms[2].material.color = Color.green;
                readyAmt++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad4) && readyBools[3] == false)
        {
            if (players[3].activeSelf)
            {
                readyBools[3] = true;
                platforms[3].material.color = Color.green;
                readyAmt++;
            }
        }

        // Scene load (back and game scenes)
        if (readyAmt >= cameraBehavior.players.Count - 4)
        {
            // LoadGame
            SceneManager.LoadScene(2);
        }
        print("ReadyAmt: " + readyAmt + "      PlayerCount: " + (cameraBehavior.players.Count - 4));

        // Back Button Scene change
        if (Radial_Button.fillAmount > 0.99f)
        {
            // load menu
            SceneManager.LoadScene(0);
        }
    }
}
