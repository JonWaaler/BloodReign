using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSettings : MonoBehaviour
{

    public GameObject p1Gun1, p1Gun2, p1Gun3;
    public GameObject p2Gun1, p2Gun2, p2Gun3;
    public GameObject p3Gun1, p3Gun2, p3Gun3;
    public GameObject p4Gun1, p4Gun2, p4Gun3;
    public List<Transform> players;
    public List<GameObject> playersMesh; // 0 - Orb; 1 - dodge; 2 - grapple
    public PlayerSettings playerSettings;

    void Start()
    {
        p1Gun1.SetActive(false);
        p1Gun2.SetActive(false);
        p1Gun3.SetActive(false);

        p2Gun1.SetActive(false);
        p2Gun2.SetActive(false);
        p2Gun3.SetActive(false);

        p3Gun1.SetActive(false);
        p3Gun2.SetActive(false);
        p3Gun3.SetActive(false);

        p4Gun1.SetActive(false);
        p4Gun2.SetActive(false);
        p4Gun3.SetActive(false);

        if (playerSettings.gunSelection_01 == 0)
            p1Gun1.SetActive(true);
        else if (playerSettings.gunSelection_01 == 1)
            p1Gun2.SetActive(true);
        else if (playerSettings.gunSelection_01 == 2)
            p1Gun3.SetActive(true);

        if (playerSettings.gunSelection_02 == 0)
            p2Gun1.SetActive(true);
        else if (playerSettings.gunSelection_02 == 1)
            p2Gun2.SetActive(true);
        else if (playerSettings.gunSelection_02 == 2)
            p2Gun3.SetActive(true);

        if (playerSettings.gunSelection_03 == 0)
            p3Gun1.SetActive(true);
        else if (playerSettings.gunSelection_03 == 1)
            p3Gun2.SetActive(true);
        else if (playerSettings.gunSelection_03 == 2)
            p3Gun3.SetActive(true);

        if (playerSettings.gunSelection_04 == 0)
            p4Gun1.SetActive(true);
        else if (playerSettings.gunSelection_04 == 1)
            p4Gun2.SetActive(true);
        else if (playerSettings.gunSelection_04 == 2)
            p4Gun3.SetActive(true);

        // Player 1 selection
        if (playerSettings.characterSelection_01 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;
        }
        else if (playerSettings.characterSelection_01 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;

            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;
        }
        else if (playerSettings.characterSelection_01 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;
        }
        else if (playerSettings.characterSelection_01 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[4]);
            playerInst.transform.SetParent(players[0]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            // clown boy
        }


        // Player 2 selection
        if (playerSettings.characterSelection_02 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;

        }
        else if (playerSettings.characterSelection_02 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;

        }
        else if (playerSettings.characterSelection_02 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;

        }
        else if (playerSettings.characterSelection_02 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[4]);
            playerInst.transform.SetParent(players[1]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        }
        
        
        // Player 3 Selection
        if (playerSettings.characterSelection_03 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;

        }
        else if (playerSettings.characterSelection_03 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;

        }
        else if (playerSettings.characterSelection_03 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;

        }
        else if (playerSettings.characterSelection_03 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[4]);
            playerInst.transform.SetParent(players[2]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        }
        
        
        // Player 4 Selection
        if (playerSettings.characterSelection_04 == 0)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[0]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.hook;

        }
        else if (playerSettings.characterSelection_04 == 1)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[1]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.teleport;

        }
        else if (playerSettings.characterSelection_04 == 2)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[3]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
            playerInst.transform.parent.GetComponent<Player>().playerEnum = PlayerAbil.roll;

        }
        else if (playerSettings.characterSelection_04 == 3)
        {
            GameObject playerInst = Instantiate<GameObject>(playersMesh[4]);
            playerInst.transform.SetParent(players[3]);
            playerInst.transform.localPosition = Vector3.zero + Vector3.up * 0.5f;
        }
    }
}
