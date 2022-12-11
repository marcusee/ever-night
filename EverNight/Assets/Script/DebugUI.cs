using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.UI;

/// <summary>
///  we should change the name of this class
/// </summary>
public class DebugUI : MonoBehaviour
{
    public List<Sprite> iconLib;

    public Text moneyAmount;
    public Text gemsAmount;
    public Text userLevel;
    public Text DebugUnitText;
    public Text DebugMessage;

    public GridLayoutGroup group;
    public Button levelUp;
    public Button summonButton;

    public Text unitListStats;
    public Image unitImage;

    public GameObject alertBox;
    public GameObject alertMessageBox;

    [HideInInspector]
    public UnitTemplate selectedUnit;

    public delegate void Lambda(); 

    // Use this for initialization
    //todo: check if null.  if null then mannually load.
    void Start () {

    }
	
    // Update is called once per frame
    void Update () {
        updateMoneyText();
        updateGemText();
        updateDebugText3();
        updateDebugUnitText();

    }
    
    /// <summary>
    ///   change method name to something more meaningful.
    /// </summary>

    void updateMoneyText()
    {
        string money = Player.Instance.money.ToString();
        
        moneyAmount.text =
            money;
    }

    void updateGemText()
    {
        string gems = Player.Instance.gems.ToString();

        gemsAmount.text =
            gems;
    }
    
    void updateDebugText3()
    {
        string level = Player.Instance.level.ToString();

        userLevel.text =
            level;
    }

    void updateDebugUnitText()
    {
        DebugUnitText.text = "Units : \n";
        for (int i = 0; i < Player.Instance.UnitTemplates.Count; i++)
            DebugUnitText.text += Player.Instance.UnitTemplates[i].ToString() + " \n";
    }
    

    public void ShowMessage(string message)
    {
        DebugMessage.text = message;
        StartCoroutine(Fade());
    }

    private IEnumerator  Fade()
    {
        yield return new WaitForSeconds(2);
        DebugMessage.text = "";
    }

    /// <summary>
    /// this part here handles the unit panel grid thing.
    /// </summary>

    public void SetUpUnitList()
    {

        if (selectedUnit != null)
        {
            var unit = selectedUnit;

            unitListStats.text = "Name: " + unit.UnitName + "\nLvl: " + unit.level + "  |  HP: " + (int)unit.HP + "\nType: " + unit.elementType +  "\n\n           Spells: \n" ;

            for (int j = 0; j < unit.spells.Count; j++)
            {
                unitListStats.text += unit.spells[j].name + "\n";
            }
                
            unitImage.sprite = iconLib[unit.IconID];
            selectedUnit = unit;
                
            Debug.Log(unit.level);
            levelUp.transform.Find("Text").GetComponent<Text>().text = "Level Up (" + 1000 * unit.level + ")";
        }
        
        int i = 0;
        foreach (UnitTemplate ut in Player.Instance.UnitTemplates)
        {
            var button = group.transform.GetChild(i);
            var icon = button.gameObject.transform.Find("ItemButton").transform.Find("Icon");
            icon.GetComponent<Image>().enabled = true;
            icon.GetComponent<Image>().sprite = iconLib[ut.IconID]; 
            i++;
        }
        // for now there is only 1 image which means that is at the 0 position.
        // add more images in the iconLib vairble in the inspector
        // then in the unit template change iconID to match the list.
        // this method is attached to the open unit button 
        setUpUnitSlot();
    }
    
    
    public void setUpUnitSlot()
    {
        
        for (int i = 0; i < 20; i++)
        {
            var islot = group.transform.GetChild(i);
            var button = islot.Find("ItemButton");
            int temp = i;
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                var unit =  Player.Instance.UnitTemplates[temp];

                unitListStats.text = "Name: " + unit.UnitName + "\nLvl: " + unit.level + "  |  HP: " + (int)unit.HP + "\nType: " + unit.elementType +  "\n\n           Spells: \n" ;

                for (int j = 0; j < unit.spells.Count; j++)
                {
                    unitListStats.text += unit.spells[j].name + "\n";
                }
                
                unitImage.sprite = iconLib[unit.IconID];
                selectedUnit = unit;
                
                levelUp.transform.Find("Text").GetComponent<Text>().text = "Level Up (" + 1000 * unit.level + ")";

            });
        }
    }


    public void ShowChoiceMessage(string msg , Lambda action)
    {
        alertBox.SetActive(true);
        var okButton = alertBox.transform.Find("OK");
        var noButton = alertBox.transform.Find("NO");
        var message = alertBox.transform.Find("Text");
        message.GetComponent<Text>().text = msg;
        
        okButton.GetComponent<Button>().onClick.RemoveAllListeners();
        noButton.GetComponent<Button>().onClick.RemoveAllListeners();
        
        
        okButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            action();
            alertBox.SetActive(false);

        });


        noButton.GetComponent<Button>().enabled = true;
        noButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            alertBox.SetActive(false);
        });

    }
    
    
    
    public void ShowNormalMessage(string msg)
    {
        alertMessageBox.SetActive(true);
        var okButton = alertMessageBox.transform.Find("OK");
        var message = alertMessageBox.transform.Find("Text");
        
        okButton.GetComponent<Button>().onClick.RemoveAllListeners();
        
        message.GetComponent<Text>().text = msg;

        okButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            alertMessageBox.SetActive(false);
        });

    }
}
