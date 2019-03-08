using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSettings : MonoBehaviour
{
    /* * * * * * * * * * * * * * * * * * * * * *
     * This is really the GameSetup script BTW *
     * * * * * * * * * * * * * * * * * * * * * */
    // This holds a reference of every gun and we just enable the gun that the player chooses
    // GUN REF's
    public GameObject p1Gun1, p1Gun2, p1Gun3, p1Gun4; // Player 1 guns
    public GameObject p2Gun1, p2Gun2, p2Gun3, p2Gun4; // Player 2 guns
    public GameObject p3Gun1, p3Gun2, p3Gun3, p3Gun4; // Player 3 guns
    public GameObject p4Gun1, p4Gun2, p4Gun3, p4Gun4; // Player 4 guns

    [Header("Reference for player")]
    public List<Transform> players;
    public List<GameObject> playersMesh;            // 0 - Orb; 1 - dodge; 2 - grapple
    public PlayerSettings playerSettings;           // The player settings that were choosen from the player selection

    [Header("UI for disabling")]
    public List<GameObject> playerHealthANDReload;  // The canvas that holds each players health and reload in game UI
    public GameObject[] ScreenUI_Health;                   // This is the other canvas that hold the "Player 1" name and health and lives
    public GameObject[] ScreenUI_Text;                   // This is the other canvas that hold the "Player 1" name and health and lives

    [Header("Elements Reference - Game setup")]
    public GameObject Element_Wind; // Shotgun
    public GameObject Element_Fire; // Sniper
    public GameObject Element_Electricity;  // Pistol
    public GameObject Element_Earth;    // Rocket Launcher
    public List<Transform> playersElements;
    void Start()
    {
        p1Gun1.SetActive(false);
        p1Gun2.SetActive(false);
        p1Gun3.SetActive(false);
        p1Gun4.SetActive(false);

        p2Gun1.SetActive(false);
        p2Gun2.SetActive(false);
        p2Gun3.SetActive(false);
        p2Gun4.SetActive(false);

        p3Gun1.SetActive(false);
        p3Gun2.SetActive(false);
        p3Gun3.SetActive(false);
        p3Gun4.SetActive(false);

        p4Gun1.SetActive(false);
        p4Gun2.SetActive(false);
        p4Gun3.SetActive(false);
        p4Gun4.SetActive(false);

        // 0 = pistol = earth
        // 1 = sniper = lightning
        // 2 = shotgun = wind
        // 3 = Rockets = Fire

        if (playerSettings.gunSelection_01 == 0)
        {
            p1Gun1.SetActive(true); // pistol
            playersElements[0] = Instantiate(Element_Earth).transform;
            playersElements[0].GetComponent<Element_FireAnimation>().gunBehavior = p1Gun1.GetComponent<GunBehavior>();
        }
        else if (playerSettings.gunSelection_01 == 1)
        {
            p1Gun2.SetActive(true); // sniper
            playersElements[0] = Instantiate(Element_Electricity).transform;

        }
        else if (playerSettings.gunSelection_01 == 2)
        {
            p1Gun3.SetActive(true); // shotgun
            playersElements[0] = Instantiate(Element_Wind).transform;

        }
        else if (playerSettings.gunSelection_01 == 3)
        {
            p1Gun4.SetActive(true); // rocket
            playersElements[0] = Instantiate(Element_Fire).transform;
            playersElements[0].GetComponent<Element_FireAnimation>().gunBehavior = p1Gun1.GetComponent<GunBehavior>();

        }
        // The fire needs 
        // "playersElements[0].GetComponent<Element_FireAnimation>().gunBehavior = p1Gun1.GetComponent<GunBehavior>();"
        // in addition. It will look the same as the earths

        if (playerSettings.gunSelection_02 == 0)
        {
            p2Gun1.SetActive(true);
            playersElements[1] = Instantiate(Element_Earth).transform;
            playersElements[1].GetComponent<Element_FireAnimation>().gunBehavior = p2Gun1.GetComponent<GunBehavior>();

        }
        else if (playerSettings.gunSelection_02 == 1)
        {
            p2Gun2.SetActive(true);
            playersElements[1] = Instantiate(Element_Electricity).transform;

        }
        else if (playerSettings.gunSelection_02 == 2)
        {
            p2Gun3.SetActive(true);
            playersElements[1] = Instantiate(Element_Wind).transform;

        }
        else if (playerSettings.gunSelection_02 == 3)
        {
            p2Gun4.SetActive(true); // rocket
            playersElements[1] = Instantiate(Element_Fire).transform;
            playersElements[1].GetComponent<Element_FireAnimation>().gunBehavior = p2Gun1.GetComponent<GunBehavior>();

        }

        if (playerSettings.gunSelection_03 == 0)
        {
            p3Gun1.SetActive(true);
            playersElements[2] = Instantiate(Element_Earth).transform;
            playersElements[2].GetComponent<Element_FireAnimation>().gunBehavior = p3Gun1.GetComponent<GunBehavior>();

        }
        else if (playerSettings.gunSelection_03 == 1)
        {
            p3Gun2.SetActive(true);
            playersElements[2] = Instantiate(Element_Electricity).transform;

        }
        else if (playerSettings.gunSelection_03 == 2)
        {
            p3Gun3.SetActive(true);
            playersElements[2] = Instantiate(Element_Wind).transform;

        }
        else if (playerSettings.gunSelection_03 == 3)
        {
            p3Gun4.SetActive(true); // rocket
            playersElements[2] = Instantiate(Element_Fire).transform;
            playersElements[2].GetComponent<Element_FireAnimation>().gunBehavior = p3Gun1.GetComponent<GunBehavior>();

        }

        if (playerSettings.gunSelection_04 == 0)
        {
            p4Gun1.SetActive(true);
            playersElements[3] = Instantiate(Element_Earth).transform;
            playersElements[3].GetComponent<Element_FireAnimation>().gunBehavior = p4Gun1.GetComponent<GunBehavior>();

        }
        else if (playerSettings.gunSelection_04 == 1)
        {
            p4Gun2.SetActive(true);
            playersElements[3] = Instantiate(Element_Electricity).transform;

        }
        else if (playerSettings.gunSelection_04 == 2)
        {
            p4Gun3.SetActive(true);
            playersElements[3] = Instantiate(Element_Wind).transform;

        }
        else if (playerSettings.gunSelection_04 == 3)
        {
            p4Gun4.SetActive(true); // rocket
            playersElements[3] = Instantiate(Element_Fire).transform;
            playersElements[3].GetComponent<Element_FireAnimation>().gunBehavior = p4Gun1.GetComponent<GunBehavior>();

        }

        // Player 1 selection
        if (playerSettings.characterSelection_01 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.hook);
        }
        else if (playerSettings.characterSelection_01 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.teleport);
        }
        else if (playerSettings.characterSelection_01 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[2]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.roll);
        }
        else if (playerSettings.characterSelection_01 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.invisible;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.invisible);
            // clown boy
        }


        // Player 2 selection
        if (playerSettings.characterSelection_02 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.hook);
        }
        else if (playerSettings.characterSelection_02 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.teleport);
        }
        else if (playerSettings.characterSelection_02 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[2]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.roll);
        }
        else if (playerSettings.characterSelection_02 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.invisible;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.invisible);
        }


        // Player 3 Selection
        if (playerSettings.characterSelection_03 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.hook);
        }
        else if (playerSettings.characterSelection_03 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.teleport);
        }
        else if (playerSettings.characterSelection_03 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[2]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.roll);
        }
        else if (playerSettings.characterSelection_03 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.invisible;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.invisible);
        }


        // Player 4 Selection
        if (playerSettings.characterSelection_04 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.hook);
        }
        else if (playerSettings.characterSelection_04 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.teleport);
        }
        else if (playerSettings.characterSelection_04 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[2]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.roll);
        }
        else if (playerSettings.characterSelection_04 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.invisible;
            playerInst.transform.parent.GetComponent<Player>().switchPlayer(PlayerAbil.invisible);
        }

        // This makes sure we donnot have game UI that is not nessasary
        if (!playerSettings.playerActive_01)
        {
            playerHealthANDReload[0].SetActive(false);
            ScreenUI_Health[0].SetActive(false);
            ScreenUI_Text[0].SetActive(false);
            playersElements[0].gameObject.SetActive(false);
        }
        if (!playerSettings.playerActive_02)
        {
            playerHealthANDReload[1].SetActive(false);
            ScreenUI_Health[1].SetActive(false);
            ScreenUI_Text[1].SetActive(false);
            playersElements[1].gameObject.SetActive(false);

        }
        if (!playerSettings.playerActive_03)
        {
            playerHealthANDReload[2].SetActive(false);
            ScreenUI_Health[2].SetActive(false);
            ScreenUI_Text[2].SetActive(false);
            playersElements[2].gameObject.SetActive(false);

        }
        if (!playerSettings.playerActive_04)
        {
            playerHealthANDReload[3].SetActive(false);
            ScreenUI_Health[3].SetActive(false);
            ScreenUI_Text[3].SetActive(false);
            playersElements[3].gameObject.SetActive(false);

        }


        // Assigning each player their element reference Transform
        players[0].GetComponent<Player>().elementRef = playersElements[0];
        players[1].GetComponent<Player>().elementRef = playersElements[1];
        players[2].GetComponent<Player>().elementRef = playersElements[2];
        players[3].GetComponent<Player>().elementRef = playersElements[3];
        // COPYIED FROM "PLAYER" FOR REFERENCE (WRITTEN IN PERSPECTIVE OF PLAYER SCRIPT)
            /* ---- How the element stuff works ----
            * "GunSettings" where the game sets itself up
            * instances an element based off of the gun type.
            * It then sets this scripts "elementRef" to what it spawned
            * Then we controll the position here to fix the stutter bug.
            */
}

}
