﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	private readonly List<GameEventListener> gameEventListeners = new List<GameEventListener>();

	public void Raise()
	{
		for (int i = gameEventListeners.Count-1; i >=0; i--)
		{
			gameEventListeners[i].OnEventRaised();
		}
	}

	public void RegisterListener(GameEventListener listener)
	{
		if (gameEventListeners.Contains(listener))
			return;
		gameEventListeners.Add(listener);
	}

	public void UnregisterListener(GameEventListener listener)
	{
		if (!gameEventListeners.Contains(listener))
			return;
		gameEventListeners.Remove(listener);
	}
}
