﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Settings")]
public class GameSettings : ScriptableObject {
    [Range(1,5)]
    public int stockCount = 3;
}