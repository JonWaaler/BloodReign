using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_Tutorial : MonoBehaviour {

    public bool Toggle_Tutorial;
    public Image Radial_Button;

    public CameraBehavior cameraBehavior;
    public List<RumblePack> readyPlayers;
    public List<bool> readyBools;
    public List<GameObject> players;
    public List<MeshRenderer> platforms;
    public int readyAmt = 0;

    // Use this for initialization
    void Start () {
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
        //if (Input.GetKey(KeyCode.B))
        //    Radial_Button.fillAmount += Time.deltaTime;
        //else
        //    Radial_Button.fillAmount -= Time.deltaTime;
        for(int i = readyPlayers.Count-1; i >= 0; i--)
        {
            if(players[i] == null)
            {
                readyPlayers.Remove(readyPlayers[i]);
                readyBools.Remove(readyBools[i]);
                players.Remove(players[i]);
            }
        }

        bool goBack = false;
        for(int i = 0; i < readyPlayers.Count; i++)
        {
            if (readyPlayers[i].state.Buttons.B == 0)
                goBack = true;             
        }
        if(goBack)
            Radial_Button.fillAmount += Time.deltaTime;
        else
            Radial_Button.fillAmount -= Time.deltaTime;

        for (int i = 0; i < readyPlayers.Count; i++)
        {
            if (readyPlayers[i].getButtonDown(0) && readyBools[i] == false)
            {
                readyBools[i] = true;
                readyAmt++;
            }
            if (readyBools[i])
                platforms[i].material.color = Color.green;
        }
        if (readyAmt >= players.Count)
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
