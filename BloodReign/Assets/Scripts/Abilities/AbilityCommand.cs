﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityCommand : MonoBehaviour {

    public string abilButton;
    public float abilCool = 5;
    public float  abilLength = 8.0f;
    public float lerpSpd = 0.5f; // smaller number is faster
    protected int layerMask = ~(1 << 2); // dont know if this works properly to ignore layer 2 (bullet layer)

    protected float nextAbil; // deltatime until
    //private bool XButtonPressed = false;
    public abstract void AbilityExcecution();
}
