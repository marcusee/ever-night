using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingLeveling : MonoBehaviour {

	// Use this for initialization
	private Text text;
	public Summoning sum;
	public genmoney genmoney;
	
	private string desc =
		"Summoning Pool level:  \n Cost to level: <insert here> \n ----------------------------------------------- Money Tree level: <insert here> \n Cost to level: <insert here> \n	";


	void Start ()
	{
		text = transform.Find("Text").gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text =
			"Summoning Pool level: "+sum.buildingLevel+" \nCost to level: "+sum.buildingUpgradeCost + " \n\n ----------------------------------------------- \n\n Money Tree level: "+ genmoney.buildingLevel + " \n Cost to level: "+ genmoney.buildingUpgradeCost+" \n	";

	}
}
