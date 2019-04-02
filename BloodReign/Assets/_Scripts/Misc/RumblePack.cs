using UnityEngine;
using XInputDotNetPure; // Required in C#
using System.Collections.Generic;

public class RumblePack : MonoBehaviour
{
    public PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    public List<float> rumbleTimerH = new List<float>();
    public List<float> rumbleTimerL = new List<float>();
    public List<float> rumbleIntensityH = new List<float>();
    public List<float> rumbleIntensityL = new List<float>();

    public float rumbleTotalH = 0.0f;
    public float rumbleTotalL = 0.0f;

    // Use this for initialization
    void Start()
    {
        rumbleTimerH = new List<float>();
        rumbleTimerL = new List<float>();
        rumbleIntensityH = new List<float>();
        rumbleIntensityL = new List<float>();
        rumbleTotalH = new float();
        rumbleTotalL = new float();
    }
    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!prevState.IsConnected)
        {
            Debug.Log(string.Format("GamePad not found {0}", playerIndex));
        }
        else
        {
            float cappedH = Mathf.Clamp01(rumbleTotalH);
            float cappedL = Mathf.Clamp01(rumbleTotalL);
            GamePad.SetVibration(playerIndex, cappedH, cappedL);
        }
        prevState = state;
        state = GamePad.GetState(playerIndex);

        // SetVibration should be sent in a slower rate.
        // Set vibration according to triggers

        for (int i = 0; i < rumbleTimerH.Count; i++)
        {
            rumbleTimerH[i] -= Time.deltaTime;
        }
        for (int i = 0; i < rumbleTimerL.Count; i++)
        {
            rumbleTimerL[i] -= Time.deltaTime;
        }

        for (int i = 0; i < rumbleTimerH.Count;)
        {
            if (rumbleTimerH[i] <= 0.0f)
            {
                rumbleTotalH -= rumbleIntensityH[i];
                rumbleTimerH.Remove(rumbleTimerH[i]);
                rumbleIntensityH.Remove(rumbleIntensityH[i]);
            }
            else
            {
                i++;
            }
        }

        for (int i = 0; i < rumbleTimerL.Count;)
        {
            if (rumbleTimerL[i] <= 0.0f)
            {
                rumbleTotalL -= rumbleIntensityL[i];
                rumbleTimerL.Remove(rumbleTimerL[i]);
                rumbleIntensityL.Remove(rumbleIntensityL[i]);
            }
            else
            {
                i++;
            }
        }

        if (rumbleTotalH < 0.0f)
            rumbleTotalH = 0.0f;
        if (rumbleTotalL < 0.0f)
            rumbleTotalL = 0.0f;

        /*
        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        */
    }
    public void addRumbleTimerH(float durationH, float intensityH)
    {
        rumbleTimerH.Add(durationH);
        rumbleIntensityH.Add(intensityH);
        rumbleTotalH += intensityH;
    }
    public void addRumbleTimerL(float durationL, float intensityL)
    {
        rumbleTimerL.Add(durationL);
        rumbleIntensityL.Add(intensityL);
        rumbleTotalL += intensityL;
    }
    public void stopRumbles()
    {
        rumbleTimerH.Clear();
        rumbleTimerL.Clear();
        rumbleTotalH = 0.0f;
        rumbleTotalL = 0.0f;
        GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
    }
    private void OnDestroy()
    {
        stopRumbles();        
    }
}