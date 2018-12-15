using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo_Manager : MonoBehaviour {

    public GameObject p1Logo1, p1Logo2, p1Logo3;
    public GameObject p2Logo1, p2Logo2, p2Logo3;
    public GameObject p3Logo1, p3Logo2, p3Logo3;
    public GameObject p4Logo1, p4Logo2, p4Logo3;

    public PlayerSettings playerSettings;

    void Awake()
    {
        p1Logo1.SetActive(false);
        p1Logo2.SetActive(false);
        p1Logo3.SetActive(false);
        p2Logo1.SetActive(false);
        p2Logo2.SetActive(false);
        p2Logo3.SetActive(false);
        p3Logo1.SetActive(false);
        p3Logo2.SetActive(false);
        p3Logo3.SetActive(false);
        p4Logo1.SetActive(false);
        p4Logo2.SetActive(false);
        p4Logo3.SetActive(false);

        if (playerSettings.playerActive_01)
        {
            if (playerSettings.characterSelection_01 == 0)
                p1Logo1.SetActive(true);
            else if (playerSettings.characterSelection_01 == 1)
                p1Logo2.SetActive(true);
            else if (playerSettings.characterSelection_01 == 2)
                p1Logo3.SetActive(true);
        }

        if (playerSettings.playerActive_02)
        {
            if (playerSettings.characterSelection_02 == 0)
                p2Logo1.SetActive(true);
            else if (playerSettings.characterSelection_02 == 1)
                p2Logo2.SetActive(true);
            else if (playerSettings.characterSelection_02 == 2)
                p2Logo3.SetActive(true);
        }

        if (playerSettings.playerActive_03)
        {
            if (playerSettings.characterSelection_03 == 0)
                p3Logo1.SetActive(true);
            else if (playerSettings.characterSelection_03 == 1)
                p3Logo2.SetActive(true);
            else if (playerSettings.characterSelection_03 == 2)
                p3Logo3.SetActive(true);
        }

        if (playerSettings.playerActive_04)
        {
            if (playerSettings.characterSelection_04 == 0)
                p4Logo1.SetActive(true);
            else if (playerSettings.characterSelection_04 == 1)
                p4Logo2.SetActive(true);
            else if (playerSettings.characterSelection_04 == 2)
                p4Logo3.SetActive(true);
        }
    }
}
