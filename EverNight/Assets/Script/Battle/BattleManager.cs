using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public BattleUI battleUI;
	private Transform playerTeam;
	private Transform enemyTeam;
	private turnSystemScript09 turnSystem;
	public bool boss;
	public GameObject bossObj;
	public List<GameObject> enemies;

	// Use this for initialization
	void Start()
	{


	}


	public void Awake()
	{
		turnSystem = GameObject.Find("Turn-basedSystem").GetComponent<turnSystemScript09>();
		playerTeam = transform.Find("PlayerTeam");
		enemyTeam = transform.Find("EnemyTeam");
		
	}

	public void StartFight()
	{
		
		turnSystem.playersGroup.Clear();
		battleUI.transform.Find("DefeatedPanel").gameObject.SetActive(false);
		battleUI.transform.Find("VictoryPanel").gameObject.SetActive(false);
		setUpPlayerUnits();
		
		if (boss)
		{

			generateReghar();
		}
		else
		{

			generateEnemies();
		}
		turnSystem.StartFight();
	}

	private void generateReghar()
	{
		GameObject instance = Instantiate( bossObj, 
			enemyTeam.GetChild(0).position  , enemyTeam.transform.rotation) as GameObject;


		var enemy = instance.AddComponent<Enemy>();
		turnSystem.playersGroup.Add(enemy);

	}
	
	private void setUpPlayerUnits()
	{
		Dictionary<string, int> unitMapper = new Dictionary<string, int>() ;
		unitMapper["EarthGolemn"] = 0;
		unitMapper["IceAppriation"] = 1;
		unitMapper["LavaSlime"] = 2;
		unitMapper["Angel"] = 3;
		unitMapper["Vampire"] = 4;

		//Player.Instance.
		for (int i = 0; i < Player.Instance.selectedParty.Length; i++)
		{

			var ut = Player.Instance.selectedParty[i];
			int index = unitMapper[ut.UnitName];
			
			GameObject instance = Instantiate( enemies[index], 
				playerTeam.GetChild(i).position  , playerTeam.transform.rotation) as GameObject;

			var player =  instance.AddComponent<PlayerMove>();
			turnSystem.playersGroup.Add(player);

			instance.GetComponent<BasicUnit>().template = ut;

		}
	}

	private void generateEnemies()
	{
		if (boss == false)
		{
	

			for (int i = 0; i < 3; i++)
			{
				int chance = Random.RandomRange(0, enemies.Count);

				GameObject instance = Instantiate( enemies[chance], 
					enemyTeam.GetChild(i).position  , enemyTeam.transform.rotation) as GameObject;


				//instance.transform.SetParent(enemyTeam , false);
				var enemy =  instance.AddComponent<Enemy>();
				turnSystem.playersGroup.Add(enemy);



			}
			
			
		}
		else
		{
			
		}
	}
}
