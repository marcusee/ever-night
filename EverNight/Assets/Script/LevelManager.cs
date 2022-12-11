using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public Camera camera;
	public BattleManager bm;
	
	private Transform homePosition;
	private Transform battlePosition;

	public GameObject home;
	public GameObject battle;
	public GameObject starting;

	// Use this for initialization
	void Start ()
	{
		homePosition = transform.Find("homeposition");
		battlePosition = transform.Find("battleposition");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
			GetComponent<AudioManager>().PlayClick();
	}

	public void GoToBattle()
	{
			foreach(var ut in Player.Instance.selectedParty)
			{
				if (ut == null) return;
			}
			AudioManager.Instance.PlayFight();
			camera.transform.position = battlePosition.position;
			camera.transform.rotation = battlePosition.rotation;


			home.SetActive(false);
			battle.SetActive(true);
		
			bm.StartFight();
	}
	public void GoToBoss()
	{
		bm.boss = true;
		GoToBattle();
	}
	
	public void GoHome()
	{
		AudioManager.Instance.PlayIntro();
		camera.transform.position = homePosition.position;
		camera.transform.rotation = homePosition.rotation;

		starting.SetActive(false);
		battle.SetActive(false);
		home.SetActive(true);
		
	}
}
