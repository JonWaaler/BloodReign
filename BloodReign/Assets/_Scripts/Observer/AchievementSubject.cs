using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSubject : Subject
{
	[SerializeField]
	private string subjectName; // Achievement name

	private void OnTriggerEnter(Collider other)
	{
		Notify(subjectName);
	}
}
