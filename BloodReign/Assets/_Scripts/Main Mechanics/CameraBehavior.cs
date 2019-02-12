using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {
    #pragma warning disable
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
        //cameraBehavior = GameObject.FindObjectOfType<CameraBehavior>();
        int index = 0;
        int tempCounter = 0;
        if (playerSettings.playerActive_01 == false)
        {
            GameObject temp = players[index].gameObject;
            players.Remove(players[index]);
            Destroy(temp);
            tempCounter++;
        }
        index++;
        if (playerSettings.playerActive_02 == false)
        {
            GameObject temp = players[index - tempCounter].gameObject;
            players.Remove(players[index]);
            Destroy(temp);
            tempCounter++;
        }
        index++;
        if (playerSettings.playerActive_03 == false)
        {
            GameObject temp = players[index - tempCounter].gameObject;
            players.Remove(players[index - tempCounter]);
            Destroy(temp);
            tempCounter++;
        }
        index++;
        if (playerSettings.playerActive_04 == false)
        {
            GameObject temp = players[index - tempCounter].gameObject;
            players.Remove(players[index - tempCounter]);
            Destroy(temp);
        }

	}


    void Update()
    {

    }

    // Update is called once per frame at end
    private void LateUpdate()
    {
        if (players.Count < 1)
            return;

        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        newPosition += new Vector3(0, 93.4838f, -65.45f); // needed to make the camera go further back from the scene
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

}
