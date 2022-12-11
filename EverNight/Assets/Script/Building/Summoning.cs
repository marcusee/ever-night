using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Summoning : MonoBehaviour
{

    public DebugUI ui;
    public int trueCost = 120;
    public int buildingLevel = 1;
    public int buildingMax = 5;
    public int buildingUpgradeCost = 1000;

    public List<UnitTemplate> UnitTemplates;

    public int cost ;

    private Text summonButton;

    public Text levelLabel;
    // Use this for initialization
    void Start ()
    {
        summonButton = ui.summonButton.transform.Find("Text").gameObject.GetComponent<Text>();
        buildingUpgradeCost = 1000 * buildingLevel;
        cost = trueCost - (buildingLevel * 20);
        
    }
    
    	
    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public void Load(int building)
    {
        buildingLevel = building;
        buildingUpgradeCost = 1000 * buildingLevel;
    }
    private void Update()
    {
        levelLabel.text = "Building Level : " + buildingLevel;
        summonButton.text = "Summon<" + (int) (trueCost - (buildingLevel * 20)) + ">";
    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Summon();
                //IncreaseLevel();
                ui.transform.Find("SummonCircleStats").gameObject.SetActive(true);
                
                /*

                */
            }
            else if (Input.GetMouseButton(1))
            {

            }
        }
    }



    public void Summon()
    {


        if (Player.Instance.UnitTemplates.Count >19 )
        {
            return;
        }
        
        cost = trueCost - (buildingLevel * 20);
        if (Player.Instance.gems < cost)
        {
            ui.ShowNormalMessage("Not Enough gems ("+ cost +")");
            return;
        }
        
        int index = Random.Range(0, UnitTemplates.Count);
      
        Debug.Log(index);
        UnitTemplate ut = Instantiate<UnitTemplate>(UnitTemplates[index]);


        
        ut.CreateUnit();
        Player.Instance.UnitTemplates.Add(ut);
        Player.Instance.gems -= cost;
  
        Debug.Log("SUMMONED");

        string msg = "You have summoned forth " + ut.UnitName +"\n"
            + "with hp " + (int) ut.HP;
        
        ui.ShowNormalMessage(msg);

        /*
        for (int i = 0; i < Player.Instance.UnitTemplates.Count; i ++ )
        {
            Debug.Log(Player.Instance.UnitTemplates[i].ToString());

        }
        */
    }

    public void IncreaseLevel()
    {
        Debug.Log(buildingUpgradeCost * buildingLevel);

        if (Player.Instance.money >= buildingUpgradeCost &&  buildingLevel < buildingMax)
        {
            Player.Instance.money -= buildingUpgradeCost;
            buildingLevel++;
            cost = trueCost - (buildingLevel * 20);
            buildingUpgradeCost = 1000 * buildingLevel;
        }
    }

    public void createUnitData()
    {
      
    }

    public string ToString()
    {
        return "Sumoning Building  level :" +  buildingLevel.ToString(); 
    }


}
