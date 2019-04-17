using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("Profile & Element UI")]
    public List<Image> profilesAndElements_Original;
    public List<Image> profilesAndElements_Placement;


    private void Start()
    {
        /* 0 - 
         * 
         * 
         * */
        
        // Set up vars
        canvas_GameUI = GameObject.Find("Canvas_GameUI");

        if (playerSettings.playerActive_01)
        {
            profilesAndElements_Placement[0].sprite = profilesAndElements_Original[playerSettings.characterSelection_01].sprite;
            //profilesAndElements_Placement[8].fillAmount = 0.5f;
            profilesAndElements_Placement[4].sprite = profilesAndElements_Original[playerSettings.gunSelection_01 + 4].sprite;
        }
        else
        {
            profilesAndElements_Placement[0].enabled = false;
            //profilesAndElements_Placement[8].enabled = false;
            profilesAndElements_Placement[4].enabled = false;
        }
        if (playerSettings.playerActive_02)
        {
            profilesAndElements_Placement[1].sprite = profilesAndElements_Original[playerSettings.characterSelection_02].sprite;
            //profilesAndElements_Placement[9].sprite = profilesAndElements_Original[playerSettings.characterSelection_02].sprite;
            profilesAndElements_Placement[5].sprite = profilesAndElements_Original[playerSettings.gunSelection_02 + 4].sprite;
        }
        else
        {
            profilesAndElements_Placement[1].enabled = false;
            //profilesAndElements_Placement[9].enabled = false;
            profilesAndElements_Placement[5].enabled = false;
        }

        if (playerSettings.playerActive_03)
        {
            profilesAndElements_Placement[2].sprite = profilesAndElements_Original[playerSettings.characterSelection_03].sprite;
            //profilesAndElements_Placement[10].sprite = profilesAndElements_Original[playerSettings.characterSelection_03].sprite;
            profilesAndElements_Placement[6].sprite = profilesAndElements_Original[playerSettings.gunSelection_03 + 4].sprite;
        }
        else
        {
            profilesAndElements_Placement[2].enabled = false;
            //profilesAndElements_Placement[10].enabled = false;
            profilesAndElements_Placement[6].enabled = false;
        }

        if (playerSettings.playerActive_04)
        {
            profilesAndElements_Placement[3].sprite = profilesAndElements_Original[playerSettings.characterSelection_04].sprite;
            //profilesAndElements_Placement[11].sprite = profilesAndElements_Original[playerSettings.characterSelection_04].sprite;
            profilesAndElements_Placement[7].sprite = profilesAndElements_Original[playerSettings.gunSelection_04 + 4].sprite;
        }
        else
        {
            profilesAndElements_Placement[3].enabled = false;
            //profilesAndElements_Placement[11].enabled = false;
            profilesAndElements_Placement[7].enabled = false;
        }

        // Give the players the amount of Heart per lifes
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
                logo_PositionsUI[1].GetChild(logo_PositionsUI[1].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);

            }
            if (playerSettings.playerActive_03)
            {
                GameObject logoInst = Instantiate(logos[4]);
                logoInst.transform.SetParent(logo_PositionsUI[2]);
                logo_PositionsUI[2].GetChild(logo_PositionsUI[2].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);

            }
            if (playerSettings.playerActive_04)
            {
                GameObject logoInst = Instantiate(logos[4]);
                logoInst.transform.SetParent(logo_PositionsUI[3]);
                logo_PositionsUI[3].GetChild(logo_PositionsUI[3].childCount - 1).localScale = new Vector3(1f, 0.5f, 1f);

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
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            
            if (cameraBehavior.players.Count <= 1)
            {
                t_delay += Time.deltaTime;


                if (t_delay > delay)
                {
                    //Restart_Canvus.SetActive(true);
                    print("Loading Scene 0: Main Menu");
                    SceneManager.LoadScene(0);
                    t_delay = 0;
                }
            }

        }
        else
        {
            // Tutorial stuff
            // For iff evveryone dies in tutorial
            if (cameraBehavior.players.Count <= 5)
            {
                t_delay += Time.deltaTime;


                if (t_delay > delay)
                {
                    //Restart_Canvus.SetActive(true);
                    print("Loading Scene 1: Tutorial");

                    SceneManager.LoadScene(1);
                    t_delay = 0;
                }
            }
        }

    }


}