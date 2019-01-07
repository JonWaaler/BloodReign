using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

    public Camera camera; 
    public List<Transform> players;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = .5f;
    public PlayerSettings playerSettings;

    [Header("Max/Min Ortho size")]
    public float maxSize = 30;
    public float minSize = 10;

    private CameraBehavior cameraBehavior;

	void Awake () {
        cameraBehavior = GameObject.FindObjectOfType<CameraBehavior>();
        int index = 0;
        int tempCounter = 0;
        if (playerSettings.playerActive_01 == false)
        {
            GameObject temp = players[index].gameObject;
            cameraBehavior.players.Remove(players[index]);
            Destroy(temp);
            tempCounter++;
        }
        index++;
        if (playerSettings.playerActive_02 == false)
        {
            GameObject temp = players[index - tempCounter].gameObject;
            cameraBehavior.players.Remove(players[index]);
            Destroy(temp);
            tempCounter++;
        }
        index++;
        if (playerSettings.playerActive_03 == false)
        {
            GameObject temp = players[index - tempCounter].gameObject;
            cameraBehavior.players.Remove(players[index - tempCounter]);
            Destroy(temp);
            tempCounter++;
        }
        index++;
        if (playerSettings.playerActive_04 == false)
        {
            GameObject temp = players[index - tempCounter].gameObject;
            cameraBehavior.players.Remove(players[index - tempCounter]);
            Destroy(temp);
        }

	}

    // Update is called once per frame
    private void LateUpdate()
    {
        if (players.Count < 1)
            return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);

        Zoom();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minSize, maxSize, GetLongestDist() / 30f);
        newZoom = Mathf.Lerp(camera.orthographicSize, newZoom, Time.deltaTime);
        camera.orthographicSize = newZoom;
    }

    Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
            return players[0].position;

        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }

        return bounds.center;
    }

    // Returns the width of the bounds
    float GetLongestDist()
    {
        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform player in players)
        {
            bounds.Encapsulate(player.position);
        }
        if (bounds.size.x > bounds.size.z)
            return bounds.size.x;
        else
            return bounds.size.z;
    }

    void Update () {







        //int count = 0;
        //int i;
        //for (i = 0; i < players.Count; i++)
        //{
        //    if(players[i] == null)
        //    {
        //        print("Player Might have been Destroyed?");
        //        count++;
        //    }
        //}
        //if(count > 0)
        //{

        //}



        //switch (players.Count)
        //{
        //    case 1:
        //        transform.position = players[0].transform.position + new Vector3(0,25,0);
        //        break;
        //    case 2:
        //        transform.position = Vector3.Lerp(new Vector3(players[0].transform.position.x, 25, players[0].transform.position.z - z_offset), 
        //                                        new Vector3(players[1].transform.position.x, 25, players[1].transform.position.z - z_offset), 
        //                                        0.5f);
        //        break;
        //    default:
        //        break;
        //}
    }
}
