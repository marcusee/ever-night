using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{


    public List<Sprite> spriteLibrary;
    
    public GameObject baseSkillButton;
    private GameObject describePanel;
    private GameObject spellPanel;
    private GameObject cooldownPanel;

    public GameObject skillToggle;
    public GameObject cooldown_label;

    private List<GameObject> actionButtonList = new List<GameObject>();
    public turnSystemScript09 turnSystem;


    private SpellSystem spellSystem;
    
	// Use this for initialization
	void Start () {
        turnSystem = GameObject.Find("Turn-basedSystem").GetComponent<turnSystemScript09>();
	    spellSystem= GameObject.Find("Turn-basedSystem").GetComponent<SpellSystem>();

        spellPanel = this.transform.Find("SpellPanel").gameObject;
        describePanel = this.transform.Find("DescriptionPanel").gameObject;
	    cooldownPanel = this.transform.Find("CoolDownIndicator").gameObject;

    }

    private void Awake()
    {
        Start();
    }


    public void UpdateUI(UnitTemplate unit)
    {
        describe(unit);
        /*
        int i = 0;
        clearPanel(spellPanel);
        foreach (SpellBase sb in unit.spells )
        {
            GameObject newSkillButton = Instantiate(baseSkillButton) as GameObject;
            Text skillButtonText = newSkillButton.transform.Find("Text").gameObject.GetComponent<Text>();
            skillButtonText.text = sb.name;
           SkillButton skillData = newSkillButton.GetComponent<SkillButton>();
           skillData.id = i;

            //if a skill's MP cost exceeds the PC's current MP, make it unselectable
            //this check isn't performed again elsewhere, so MP can go negative if this check is removed
            newSkillButton.transform.SetParent(spellPanel.transform, false);
            actionButtonList.Add(newSkillButton);
            i++;
        }
        */

        updateSpellButtons(unit);
    }



    private void updateSpellButtons(UnitTemplate unit)
    {
        
        if (unit == null) return;
        if (spellPanel == null)
        {
            Debug.Log("IS NULL BOY");
            return;
        }
        
        
        clearPanel(spellPanel);
        clearPanel(cooldownPanel);

        int i = 0;
        foreach (SpellBase sb in unit.spells )
        {


            GameObject newSkillButton = spellPanel.transform.GetChild(i).gameObject;

            if (newSkillButton == null)
            {
                Debug.Log("nullllllllllllllllllllll");
                return;
            }

            var toggle = newSkillButton.GetComponent<Toggle>();
            toggle.transform.Find("Background").
                gameObject.GetComponent<Image>().
                sprite = spriteLibrary[sb.iconId];
            
            
            
            GameObject label = cooldownPanel.transform.GetChild(i).gameObject as GameObject;
            // label.transform.SetParent(cooldownPanel.transform, false);

            toggle.group = spellPanel.GetComponent<ToggleGroup>();


            if (i == 0)
            {
                toggle.isOn = true;
            }
            
            int tempI = i;
            Debug.Log(sb.current_cooldown);
            if (sb.current_cooldown > 0)
            {
                toggle.enabled = false;
                label.gameObject.GetComponent<Text>().text =
                    sb.current_cooldown.ToString();
            }
            else
            {
                toggle.enabled = true;
                label.gameObject.GetComponent<Text>().text = "";

            }
            toggle.onValueChanged.AddListener(delegate
            {
                spellSystem.SetSpell(tempI);
            });
            
            //newSkillButton.transform.SetParent(spellPanel.transform, false);
            actionButtonList.Add(newSkillButton);
            i++;
        }
    }
    
    public void describe(UnitTemplate unit)
    {
//        describePanel.transform.GetChild(0).GetComponent<Text>().text = "name : " +  unit.name;
 //       describePanel.transform.GetChild(1).GetComponent<Text>().text = "HP : " + (int) unit.currentHP;
  //      describePanel.transform.GetChild(0).GetComponent<Text>().text = "obj name : " + turnSystem.currentOBJ.name;

    }

    // Update is called once per frame
    void Update () {
		
	}
    
    
    public void deactivateSelector(GameObject obj)
    {
        obj.transform.Find("Selector").gameObject.SetActive(false);
    }

    public void activateSelector(GameObject obj)
    {
        obj.transform.Find("Selector").gameObject.SetActive(true);
    }

    private void clearPanel(GameObject panel)
    {
        if (panel == null) return;                
        foreach (Transform child in panel.transform)
        {
          //  GameObject.Destroy(child.gameObject);
        }
    }
}
