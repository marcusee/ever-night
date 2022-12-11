using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreen : MonoBehaviour
{
	public bool newGame = false;

	public GameObject dialouge;
	public GameObject tutorialPanel;
	public GameObject tutorialPanelCombat;

	// Use this for initialization
	void Start () {
		if (newGame)
		{
			
			dialouge.SetActive(true);
			
		}

		newGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartNewGame()
	{
		
	}

	public void ContinueGame()
	{
	}
}
