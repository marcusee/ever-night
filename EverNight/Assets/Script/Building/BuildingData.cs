using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour {

    public int trueCost = 120;
    public int buildingLevel = 1;
    public int buildingMax = 5;
    public int buildingUpgradeCost = 1000;
    private int cost;


    void Start()
    {
        cost = trueCost - (buildingLevel * 20);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void IncreaseLevel()
    {

    }
}
