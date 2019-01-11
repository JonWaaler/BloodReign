using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject playerSelect;

    public GameObject[] playerTXT;
    public GameObject[] pressA;
    public GameObject[] grappleIMG;
    public GameObject[] orbIMG;
    public GameObject[] rollIMG;
    public GameObject[] pistolIMG;
    public GameObject[] sniperIMG;
    public GameObject[] shotgunIMG;
    public GameObject[] rocketIMG;
    public GameObject[] charPointers;
    public GameObject[] gunPointers;
    public CanvasGroup PRESSSTART;

    private bool[] CharacterSelect;
    private bool[] GunSelect;
    private bool[] axisInUse;
    private bool[] playerWeapon;
    private bool[] ready;
    private int[] charCounter;
    private int[] gunCounter;
    private int readyPlayers;
    private int joinedPlayers;
    private float fade = 1f;
    private bool Fade = true;

    public PlayerSettings playerSettings;

    List<int> controllers = new List<int>();

    void Awake()
    {
        CharacterSelect = new bool[4];
        GunSelect = new bool[4];
        axisInUse = new bool[4];
        playerWeapon = new bool[4];
        ready = new bool[4];
        charCounter = new int[4];
        gunCounter = new int[4];

        readyPlayers = 0;
        joinedPlayers = 0;

        // Set everything false by default except for pressA
        for (int i = 0; i < 4; i++)
        {
            playerTXT[i].SetActive(false);
            pressA[i].SetActive(true);
            grappleIMG[i].SetActive(false);
            orbIMG[i].SetActive(false);
            rollIMG[i].SetActive(false);
            pistolIMG[i].SetActive(false);
            sniperIMG[i].SetActive(false);
            shotgunIMG[i].SetActive(false);
            rocketIMG[i].SetActive(false);
            charPointers[i].SetActive(false);
            gunPointers[i].SetActive(false);

            CharacterSelect[i] = false;
            GunSelect[i] = false;
            axisInUse[i] = false;
            playerWeapon[i] = false;
            charCounter[i] = 0;
            gunCounter[i] = 0;
        }
        PRESSSTART.gameObject.SetActive(false);
    }

    void Update()
    {
        Debug.Log("joinedPlayers: " + joinedPlayers + " || readyPlayers: " + readyPlayers);
        // ---- Back Button ---- //
        for (int i = 0; i < 4; i++)
        {
            // If BButton pressed then go back to main menu
            if (Input.GetKeyDown("joystick " + (i + 1) + " button 1") && controllers.Contains(i + 1) && !GunSelect[i])
                SceneManager.LoadScene(0);
            else if (Input.GetKeyDown("joystick " + (i + 1) + " button 1") && controllers.Contains(i + 1) && GunSelect[i])
            {
                // Go back to character selection
                pistolIMG[i].SetActive(false);
                sniperIMG[i].SetActive(false);
                shotgunIMG[i].SetActive(false);
                rocketIMG[i].SetActive(false);
                gunPointers[i].SetActive(false);
                GunSelect[i] = false;
                CharacterSelect[i] = true;
            }
        }

        // ---- Gun Selection Player ---- //
        for (int i = 0; i < 4; i++)
        {
            if (GunSelect[i])
            {
                // Reseting images
                pistolIMG[i].SetActive(false);
                sniperIMG[i].SetActive(false);
                shotgunIMG[i].SetActive(false);
                rocketIMG[i].SetActive(false);
                gunPointers[i].SetActive(true);

                // Increment gun counter (LS Right)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 1)
                {
                    if (axisInUse[i] == false)
                    {
                        gunCounter[i]++;
                        axisInUse[i] = true;
                    }
                }

                // Increment gun counter (LS Left)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == -1)
                {
                    if (axisInUse[i] == false)
                    {
                        gunCounter[i]--;
                        axisInUse[i] = true;
                    }
                }
                // Do not Increment gun counter and axis is not in use
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 0)
                    axisInUse[i] = false;

                // Gun counter range only 0, 1 and 2
                gunCounter[i] = (gunCounter[i] < 0) ? 2 : gunCounter[i];
                gunCounter[i] = (gunCounter[i] > 2) ? 0 : gunCounter[i];

                // Set gun images
                if (gunCounter[i] == 0)
                    pistolIMG[i].SetActive(true);
                else if (gunCounter[i] == 1)
                    sniperIMG[i].SetActive(true);
                else if (gunCounter[i] == 2)
                    shotgunIMG[i].SetActive(true);
                else if (gunCounter[i] == 3)
                    rocketIMG[i].SetActive(true);

                // Confirm Gun Selection
                if (GunSelect[i] == true && ready[i] == false)
                {
                    ready[i] = true;
                    joinedPlayers--;
                    readyPlayers++;
                    // Maybe make a readyplayers state
                }

                // If all joined players are ready flash press start text
                if ((joinedPlayers == 0) && (readyPlayers >= 2))
                {
                    PRESSSTART.gameObject.SetActive(true);
                    // Text fade in and out
                    if (fade >= 1f)
                        Fade = true;
                    else if (fade <= 0.25f)
                        Fade = false;

                    if (Fade)
                        fade -= Time.deltaTime;
                    else
                        fade += Time.deltaTime;
                    PRESSSTART.alpha = fade;
                }
                else
                {
                    PRESSSTART.gameObject.SetActive(false);
                }

                //If Start button is pressed and all joined players are ready
                if (Input.GetKeyDown("joystick " + (i + 1) + " button 7") && (joinedPlayers == 0) && (readyPlayers >= 2))
                {
                    GunSelect[i] = false;
                    gunPointers[i].SetActive(false);

                    // ---- Update Current Settings ---- //
                    playerSettings.playerActive_01 = playerTXT[0].activeSelf;
                    playerSettings.playerActive_02 = playerTXT[1].activeSelf;
                    playerSettings.playerActive_03 = playerTXT[2].activeSelf;
                    playerSettings.playerActive_04 = playerTXT[3].activeSelf;

                    playerSettings.characterSelection_01 = charCounter[0];
                    playerSettings.characterSelection_02 = charCounter[1];
                    playerSettings.characterSelection_03 = charCounter[2];
                    playerSettings.characterSelection_04 = charCounter[3];

                    playerSettings.gunSelection_01 = gunCounter[0];
                    playerSettings.gunSelection_02 = gunCounter[1];
                    playerSettings.gunSelection_03 = gunCounter[2];
                    playerSettings.gunSelection_04 = gunCounter[3];

                    SceneManager.LoadScene(1);
                }
            }
            // if ready player deselects, disable press start canvas
            if (GunSelect[i] == false && ready[i] == true)
            {
                ready[i] = false;
                joinedPlayers++;
                readyPlayers--;
            }
        }

        // ---- Character Selection ---- //
        for (int i = 0; i < 4; i++)
        {
            if (CharacterSelect[i])
            {
                // Reseting images
                grappleIMG[i].SetActive(false);
                orbIMG[i].SetActive(false);
                rollIMG[i].SetActive(false);
                charPointers[i].SetActive(true);

                // Increment character counter (LS Right)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 1)
                {
                    Debug.Log(1);
                    if (axisInUse[i] == false)
                    {
                        charCounter[i]++;
                        axisInUse[i] = true;
                    }
                }
                // Increment character counter (LS Left)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == -1)
                {
                    Debug.Log(-1);
                    if (axisInUse[i] == false)
                    {
                        charCounter[i]--;
                        axisInUse[i] = true;
                    }
                }
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 0)
                {
                    Debug.Log(0);
                    axisInUse[i] = false;
                }

                charCounter[i] = (charCounter[i] > 2) ? 0 : charCounter[i];
                charCounter[i] = (charCounter[i] < 0) ? 2 : charCounter[i];

                if (charCounter[i] == 0)
                    grappleIMG[i].SetActive(true);
                else if (charCounter[i] == 1)
                    orbIMG[i].SetActive(true);
                else if (charCounter[i] == 2)
                    rollIMG[i].SetActive(true);

                if (Input.GetKeyDown("joystick " + (i + 1) + " button 0"))
                {
                    CharacterSelect[i] = false;
                    GunSelect[i] = true;
                    charPointers[i].SetActive(false);
                }
            }
        }

        // ---- Add Players ---- //
        if (playerSelect.activeSelf && !(mainMenu.activeSelf))
        {
            for (int i = 0; i < 4; i++)
            {
                if (Input.GetKeyDown("joystick " + (i + 1) + " button 0"))
                {
                    // if our list of controllers doesnt contain the new controller then...
                    if (!controllers.Contains(i + 1))
                    {
                        controllers.Add(i + 1);
                        joinedPlayers++;

                        // Set player text to active
                        playerTXT[i].SetActive(true);
                        pressA[i].SetActive(false);
                        // Player will be in the game so we set this to true
                        CharacterSelect[i] = true;

                    }
                }
            }
        }
    }
}
