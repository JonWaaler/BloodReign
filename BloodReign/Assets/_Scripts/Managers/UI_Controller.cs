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
    public GameObject[] invisIMG;
    public GameObject[] pistolIMG;
    public GameObject[] sniperIMG;
    public GameObject[] shotgunIMG;
    public GameObject[] rocketIMG;
    public GameObject[] charPointers;
    public GameObject[] gunPointers;
    public CanvasGroup PRESSSTART;
    public GameObject[] readyIMG;

    public GameObject[] Card;
    public GameObject[] CardBack;
    private Animator[] CardAnim;
    private Animator[] CardBackAnim;

    [Header("Sound Manager")]
    public SoundManager soundManager;
    public Sounds.SoundName menu_move;
    public Sounds.SoundName readyup;
    public Sounds.SoundName menuMusic;

    private bool[] CharacterSelect;
    private bool[] GunSelect;
    private bool[] ReadySelect;
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

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        CardAnim = new Animator[4];
        CardBackAnim = new Animator[4];
        for (int i = 0; i < 4; i++)
        {
            CardAnim[i] = Card[i].GetComponent<Animator>();
            CardBackAnim[i] = CardBack[i].GetComponent<Animator>();
            //CardAnim[i].SetBool("Flip", false);
            //CardBackAnim[i].SetBool("Flip", false);
        }
    }

    private void Awake()
    {
        CharacterSelect = new bool[4];
        GunSelect = new bool[4];
        ReadySelect = new bool[4];
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
            grappleIMG[i].SetActive(true);
            orbIMG[i].SetActive(true);
            rollIMG[i].SetActive(true);
            invisIMG[i].SetActive(true);
            pistolIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            sniperIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            shotgunIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            rocketIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
            charPointers[i].SetActive(false);
            gunPointers[i].SetActive(false);
            readyIMG[i].SetActive(false);

            CharacterSelect[i] = false;
            GunSelect[i] = false;
            axisInUse[i] = false;
            playerWeapon[i] = false;
            charCounter[i] = 0;
            gunCounter[i] = 0;
        }
        PRESSSTART.gameObject.SetActive(false);

    }

    private void Update()
    {
        Debug.Log("joinedPlayers: " + joinedPlayers + " || readyPlayers: " + readyPlayers);
        // ---- Back Button ---- //
        for (int i = 0; i < 4; i++)
        {
            // If BButton pressed then go back to main menu
            if (Input.GetKeyDown("joystick " + (i + 1) + " button 1") && controllers.Contains(i + 1) && !GunSelect[i] && !ReadySelect[i])
                SceneManager.LoadScene(0);
            else if (Input.GetKeyDown("joystick " + (i + 1) + " button 1") && controllers.Contains(i + 1) && GunSelect[i] && !ReadySelect[i])
            {
                // Go back to character selection
                pistolIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                sniperIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                shotgunIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                rocketIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                GunSelect[i] = false;
                CharacterSelect[i] = true;
            }
            else if (Input.GetKeyDown("joystick " + (i + 1) + " button 1") && controllers.Contains(i + 1) && !GunSelect[i] && ReadySelect[i])
            {
                // Go back to gun selection
                readyIMG[i].SetActive(false);
                ReadySelect[i] = false;
                GunSelect[i] = true;
            }
        }

        // ---- Gun Selection Player ---- //
        for (int i = 0; i < 4; i++)
        {
            if (ReadySelect[i])
            {
                readyIMG[i].SetActive(true);

                if (ReadySelect[i] == true && ready[i] == false)
                {
                    ready[i] = true;
                    joinedPlayers--;
                    readyPlayers++;
                    soundManager.Play(readyup);
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

                    soundManager.Stop(Sounds.SoundName.Menu_Music);
                    SceneManager.LoadScene(1);
                }
            }

            if (GunSelect[i])
            {
                // Increment gun counter (LS Right)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 1)
                {
                    if (axisInUse[i] == false)
                    {
                        gunCounter[i]++;
                        axisInUse[i] = true;
                        soundManager.Play(menu_move);
                    }
                }

                // Increment gun counter (LS Left)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == -1)
                {
                    if (axisInUse[i] == false)
                    {
                        gunCounter[i]--;
                        axisInUse[i] = true;
                        soundManager.Play(menu_move);
                    }
                }
                // Do not Increment gun counter and axis is not in use
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 0)
                    axisInUse[i] = false;

                // Gun counter range only 0, 1 and 2
                gunCounter[i] = (gunCounter[i] < 0) ? 3 : gunCounter[i];
                gunCounter[i] = (gunCounter[i] > 3) ? 0 : gunCounter[i];

                // Set gun images
                pistolIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                sniperIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                shotgunIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                rocketIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                if (gunCounter[i] == 0)
                    pistolIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 1f);
                else if (gunCounter[i] == 1)
                    sniperIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 1f);
                else if (gunCounter[i] == 2)
                    shotgunIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 1f);
                else if (gunCounter[i] == 3)
                    rocketIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0.15f, 0.15f, 1f);

                if (Input.GetKeyDown("joystick " + (i + 1) + " button 0"))
                {
                    GunSelect[i] = false;
                    ReadySelect[i] = true;
                }
            }
            // if ready player deselects, disable press start canvas
            if (ReadySelect[i] == false && ready[i] == true)
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
                //charPointers[i].SetActive(true);

                // Increment character counter (LS Right)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 1)
                {
                    //Debug.Log(1);
                    if (axisInUse[i] == false)
                    {
                        charCounter[i]++;
                        axisInUse[i] = true;
                        soundManager.Play(menu_move);
                    }
                }
                // Increment character counter (LS Left)
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == -1)
                {
                    if (axisInUse[i] == false)
                    {
                        charCounter[i]--;
                        axisInUse[i] = true;
                        soundManager.Play(menu_move);
                    }
                }
                if (Input.GetAxisRaw("H_LStick" + (i + 1)) == 0)
                {
                    axisInUse[i] = false;
                }

                charCounter[i] = (charCounter[i] > 3) ? 0 : charCounter[i];
                charCounter[i] = (charCounter[i] < 0) ? 3 : charCounter[i];

                grappleIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                orbIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                rollIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
                invisIMG[i].GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);

                if (charCounter[i] == 0)
                    grappleIMG[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                else if (charCounter[i] == 1)
                    orbIMG[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                else if (charCounter[i] == 2)
                    rollIMG[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                else if (charCounter[i] == 3)
                    invisIMG[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                Debug.Log(charCounter[0]);

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

                        CardAnim[i].SetBool("Flip", true);
                        CardBackAnim[i].SetBool("Flip", true);
                        grappleIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        orbIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        rollIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        invisIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        pistolIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        sniperIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        shotgunIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                        rocketIMG[i].GetComponent<Animator>().SetBool("Flip", true);
                    }
                }
            }
        }
    }
}
