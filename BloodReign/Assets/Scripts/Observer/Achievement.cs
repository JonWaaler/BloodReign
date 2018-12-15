using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : Observer
{
	private bool achievement = false;
	private Vector3 achievementPos = new Vector3(300f, -270f, 0f);
	private float time = 0f;

	// Use this for initialization
	private void Start ()
	{
		PlayerPrefs.DeleteAll();
		foreach (var subject in FindObjectsOfType<AchievementSubject>()) // for each achievemnt subject...
		{
			subject.AddObserver(this);
		}
	}

	public override void OnNotify(object value)
	{
		string achievementKey = "Achievement_" + value;

		if (PlayerPrefs.GetInt(achievementKey) == 1) // if achievement is already unlocked
			return;

		PlayerPrefs.SetInt(achievementKey, 1);
		Debug.Log("Achievement - " + value);
		// --- pop up achievement ---
		achievement = true;
	}

	void FixedUpdate()
	{
		if (achievement == true)
		{
			//GameObject.Find("Achievement Image").GetComponent<RectTransform>().localPosition = new Vector3(300f, -180f, 0f);
			// lerp between (300f, -270f, 0f) and (300f, -180f, 0f)
			time += Time.deltaTime;
			GameObject.Find("Achievement Image").GetComponent<RectTransform>().localPosition = achievementPos;
			if (achievementPos.y < -180f && time < 5f)
			{
				achievementPos.y += Time.deltaTime * 50f;
			}

			if (achievementPos.y > -270 && time > 5f)
			{
				achievementPos.y -= Time.deltaTime * 50f;
			}
		}
	}
}
