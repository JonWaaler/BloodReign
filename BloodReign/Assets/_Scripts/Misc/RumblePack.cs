using UnityEngine;
using XInputDotNetPure; // Required in C#
using System.Collections.Generic;

public class RumblePack : MonoBehaviour
{
    public PlayerIndex playerIndex;
    public GamePadState state;
    public GamePadState prevState;
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
    public bool getButtonDown(int inputID)
    {
        switch (inputID)
        {
            case 0:
                if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
                    return true;
                break;
            case 1:
                if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
                    return true;
                break;
            case 2:
                if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
                    return true;
                break;
            case 3:
                if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed)
                    return true;
                break;
            case 4:
                if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
                    return true;
                break;
            case 5:
                if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
                    return true;
                break;
            case 6:
                if (prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed)
                    return true;
                break;
            case 7:
                if (prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed)
                    return true;
                break;
            case 8:
                if (prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Pressed)
                    return true;
                break;
            case 9:
                if (prevState.Buttons.RightStick == ButtonState.Released && state.Buttons.RightStick == ButtonState.Pressed)
                    return true;
                break;
            case 10:
                if (prevState.Triggers.Left == 0 && state.Triggers.Left > 0)
                    return true;
                break;
            case 11:
                if (prevState.Triggers.Right == 0 && state.Triggers.Right> 0)
                    return true;
                break;
            default:
                return false;
        }
        return false;
    }
    public bool getButtonUp(int inputID)
    {
        switch (inputID)
        {
            case 0:
                if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
                    return true;
                break;
            case 1:
                if (prevState.Buttons.B == ButtonState.Pressed && state.Buttons.B == ButtonState.Released)
                    return true;
                break;
            case 2:
                if (prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Released)
                    return true;
                break;
            case 3:
                if (prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released)
                    return true;
                break;
            case 4:
                if (prevState.Buttons.RightShoulder == ButtonState.Pressed && state.Buttons.RightShoulder == ButtonState.Released)
                    return true;
                break;
            case 5:
                if (prevState.Buttons.LeftShoulder == ButtonState.Pressed && state.Buttons.LeftShoulder == ButtonState.Released)
                    return true;
                break;
            case 6:
                if (prevState.Buttons.Back == ButtonState.Pressed && state.Buttons.Back == ButtonState.Released)
                    return true;
                break;
            case 7:
                if (prevState.Buttons.Start == ButtonState.Pressed && state.Buttons.Start == ButtonState.Released)
                    return true;
                break;
            case 8:
                if (prevState.Buttons.LeftStick == ButtonState.Pressed && state.Buttons.LeftStick == ButtonState.Released)
                    return true;
                break;
            case 9:
                if (prevState.Buttons.RightStick == ButtonState.Pressed && state.Buttons.RightStick == ButtonState.Released)
                    return true;
                break;
            case 10:
                if (prevState.Triggers.Left > 0 && state.Triggers.Left == 0)
                    return true;
                break;
            case 11:
                if (prevState.Triggers.Right > 0 && state.Triggers.Right == 0)
                    return true;
                break;
            default:
                return false;
        }
        return false;
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
    public void changePlayerToRumble()
    {
        if ((int)playerIndex < 3)
        {
            playerIndex++;
        }
        else
        {
            playerIndex = PlayerIndex.One;
        }
    }
}