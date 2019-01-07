using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
	public abstract void OnNotify(object value);
}

public abstract class Subject : MonoBehaviour
{
	private List<Observer> observers = new List<Observer>();

	// Notify all observers
	public void Notify(object value)
	{
		for (int i = 0; i < observers.Count; i++)
		{
			observers[i].OnNotify(value);
		}
	}

	public void AddObserver(Observer observer)
	{
		observers.Add(observer);
	}
}
