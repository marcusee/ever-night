using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class genmoney : MonoBehaviour
{
	public DebugUI ui;
	[HideInInspector]
	public DateTime timeStamp;

	private int moneybounty = 100;
    public int buildingLevel = 1;
    public int buildingMax = 5;
    public int buildingUpgradeCost = 1000;

    // Use this for initialization
    void Start () {

		timeStamp = DateTime.MinValue;
	    buildingUpgradeCost = 1000 * buildingLevel;

	}

	public void Load(int building)
	{
		buildingLevel = building;
		buildingUpgradeCost = 1000 * buildingLevel;
	}
	
	public void Load(DateTime time)
	{
		timeStamp = time;
	}
	// Update is called once per frame
	void Update () {
		
	}
	
	
	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
	
	private void OnMouseDown()
	{
		if (!IsPointerOverUIObject())
		{

			var diff = System.DateTime.Now - timeStamp;

			if (diff.Minutes >= 1)
			{
				ui.ShowNormalMessage("You have \n recived " + moneybounty + " gems ");
				Player.Instance.gems += moneybounty;
				timeStamp = System.DateTime.Now;
			}
		}
	}

    public void IncreaseLevel()
    {
        Debug.Log(buildingUpgradeCost * buildingLevel);

        if (Player.Instance.money >= buildingUpgradeCost && buildingLevel < buildingMax)
        {
            Player.Instance.money -= buildingUpgradeCost;
            buildingLevel++;
            buildingUpgradeCost = 1000 * buildingLevel;
            moneybounty = 100 * buildingLevel;
        }
    }
}
