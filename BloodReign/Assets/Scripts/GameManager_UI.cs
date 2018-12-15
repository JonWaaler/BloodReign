using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager_UI : MonoBehaviour {

    // UI Manager for
    public GameObject Restart_Canvus;
    private CameraBehavior cameraBehavior;
    private float t_delay = 0;
    public float delay = 3;

    private void Start()
    {
        cameraBehavior = FindObjectOfType<CameraBehavior>();

        if (cameraBehavior == null)
            print("<color = red> GameManager_UI can't find 'CameraBehavior'</color>");
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

    //public void Restart()
    //{
    //    print("HELLO WORLD");
    //}
}
