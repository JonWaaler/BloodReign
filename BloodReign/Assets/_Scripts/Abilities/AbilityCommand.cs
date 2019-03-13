using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityCommand : MonoBehaviour
{
    public float abilCool;
    public AbilitySettings abilSettings;
    protected int layerMask = ~(1 << 2); // dont know if this works properly to ignore layer 2 (bullet layer)
    //private bool XButtonPressed = false;
    public abstract void AbilityExcecution();
    public abstract void ResetSphere();
}