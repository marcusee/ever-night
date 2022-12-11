using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
	public LevelManager manager;
	public GameObject startOption;
	public GameObject confirmObj;
	public HomeScreen home;

	public SaveLoad saveLoad;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			//startOption.SetActive(true);
		}	
	}

	public void StartNewGame()
	{
		home.newGame = true;
		manager.GoHome();
	}
	
	public void ContinueGame()
	{
		saveLoad.loadOnStart = true;
		manager.GoHome();
	}
}
