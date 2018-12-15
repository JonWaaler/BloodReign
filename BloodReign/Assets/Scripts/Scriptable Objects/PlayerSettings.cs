using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Settings", menuName = "Player Settings")]
public class PlayerSettings : ScriptableObject {

	public bool playerActive_01;
	public int characterSelection_01;
	public int gunSelection_01;

	public bool playerActive_02;
	public int characterSelection_02;
	public int gunSelection_02;

	public bool playerActive_03;
	public int characterSelection_03;
	public int gunSelection_03;

	public bool playerActive_04;
	public int characterSelection_04;
	public int gunSelection_04;
}
