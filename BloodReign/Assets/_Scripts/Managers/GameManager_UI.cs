using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_UI : MonoBehaviour {

    // UI Manager
    public GameObject Restart_Canvus;
    private CameraBehavior cameraBehavior;
    private float t_delay = 0;
    public float delay = 3;

    // Scriptable Objects
    [Header("Scriptable objects")]
    public PlayerSettings playerSettings;
    public GameSettings gameSettings;

    // Game magager - LIVES
    private GameObject canvas_GameUI;
    public List<GameObject> logos;  // Character faces
    public List<Transform> logo_PositionsUI;    // The game that holds the logos

    private void Start()
    {
        // Set up vars
        canvas_GameUI = GameObject.Find("Canvas_GameUI");

        if (playerSettings.playerActive_01)
        {
            GameObject profileInst = Instantiate(logos[playerSettings.characterSelection_01]);
            profileInst.transform.SetParent(logo_PositionsUI[4]);
            //GameObject abilityInst = Instantiate(logos[playerSettings.gunSelection_01 + 5], new Vector3(500, 0, 0), Quaternion.Euler(0,0,0)); WHY CANT I MOVE THIS!
            GameObject abilityInst = Instantiate(logos[playerSettings.gunSelection_01 + 5]);
            abilityInst.transform.SetParent(logo_PositionsUI[4]);
        }
        if (playerSettings.playerActive_02)
        {
            GameObject profileInst = Instantiate(logos[playerSettings.characterSelection_02]);
            profileInst.transform.SetParent(logo_PositionsUI[5]);
            GameObject abilityInst = Instantiate(logos[playerSettings.gunSelection_02 + 5]);
            abilityInst.transform.SetParent(logo_PositionsUI[5]);
        }
        if (playerSettings.playerActive_03)
        {
            GameObject profileInst = Instantiate(logos[playerSettings.characterSelection_03]);
            profileInst.transform.SetParent(logo_PositionsUI[6]);
            GameObject abilityInst = Instantiate(logos[playerSettings.gunSelection_03 + 5]);
            abilityInst.transform.SetParent(logo_PositionsUI[6]);
        }
        if (playerSettings.playerActive_04)
        {
            GameObject profileInst = Instantiate(logos[playerSettings.characterSelection_04]);
            profileInst.transform.SetParent(logo_PositionsUI[7]);
            GameObject abilityInst = Instantiate(logos[playerSettings.gunSelection_04 + 5]);
            abilityInst.transform.SetParent(logo_PositionsUI[7]);
        }

        // Give the players the amount of logos per lifes
        for (int i = 0; i < gameSettings.stockCount; i++)
        {
            if (playerSettings.playerActive_01)
            {
                GameObject logoInst = Instantiate(logos[4]);
                logoInst.transform.SetParent(logo_PositionsUI[0]);
                // Was a scaling issue where it change so i gotta set the scale after i parent it to the grouping object
                logo_PositionsUI[0].GetChild(logo_PositionsUI[0].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);
            }
            if (playerSettings.playerActive_02)
            {
                GameObject logoInst = Instantiate(logos[4]);
                logoInst.transform.SetParent(logo_PositionsUI[1]);
                logo_PositionsUI[1].GetChild(logo_PositionsUI[0].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);

            }
            if (playerSettings.playerActive_03)
            {
                GameObject logoInst = Instantiate(logos[4]);
                logoInst.transform.SetParent(logo_PositionsUI[2]);
                logo_PositionsUI[2].GetChild(logo_PositionsUI[0].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);

            }
            if (playerSettings.playerActive_04)
            {
                GameObject logoInst = Instantiate(logos[4]);
                logoInst.transform.SetParent(logo_PositionsUI[3]);
                logo_PositionsUI[3].GetChild(logo_PositionsUI[0].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);

            }
        }

        cameraBehavior = FindObjectOfType<CameraBehavior>();
        if (cameraBehavior == null)
            print("<color = red> GameManager_UI can't find 'CameraBehavior'</color>");
    }


    // Will return true if player still has lives false if no lives are left
    public bool RemoveLife(int player)
    {
        // 0 is player 1..... etc
        switch (player)
        {
            case 0:
                Destroy(logo_PositionsUI[0].GetChild(logo_PositionsUI[0].childCount - 1).gameObject);
                if (logo_PositionsUI[0].childCount > 1)
                {

                    return true;
                }
                else
                    return false;
            case 1:
                Destroy(logo_PositionsUI[1].GetChild(logo_PositionsUI[1].childCount - 1).gameObject);
                if (logo_PositionsUI[1].childCount > 1)
                {

                    return true;
                }
                else
                    return false;
            case 2:
                Destroy(logo_PositionsUI[2].GetChild(logo_PositionsUI[2].childCount - 1).gameObject);
                if (logo_PositionsUI[2].childCount > 1)
                {

                    return true;
                }
                else
                    return false;
            case 3:
                Destroy(logo_PositionsUI[3].GetChild(logo_PositionsUI[3].childCount - 1).gameObject);
                if (logo_PositionsUI[3].childCount > 1)
                {

                    return true;
                }
                else
                    return false;
            default:
                Debug.LogError("Remove Life in 'GameManager_UI' was not given the right number", this);
                return false;
        }

    }

    public void Update()
    {
        if (cameraBehavior.players.Count <= 1)
        {
            t_delay += Time.deltaTime;


            if(t_delay > delay){
                //Restart_Canvus.SetActive(true);
                
                SceneManager.LoadScene(0);
                t_delay = 0;
            }
        }

    }


}