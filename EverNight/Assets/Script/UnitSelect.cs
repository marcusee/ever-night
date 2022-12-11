using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelect : MonoBehaviour {

	public GridLayoutGroup group;
	public List<Button> partyButtons;

	private List<Sprite> iconLib;

	private int selectedCount = 0;

	private UnitTemplate[] party;
	// Use this for initialization
	void Start () {
		party = new UnitTemplate[3];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Awake()
	{
		Debug.Log("I'M AWAKE");
	}

	public void OpenList()
	{
		iconLib = transform.parent.gameObject.GetComponent<DebugUI>().iconLib;

		int i = 0;
		foreach (UnitTemplate ut in Player.Instance.UnitTemplates)
		{
			var button = group.transform.GetChild(i);
			var icon = button.gameObject.transform.Find("ItemButton").transform.Find("Icon");
			icon.GetComponent<Image>().enabled = true;
			Debug.Log(ut.IconID);
			icon.GetComponent<Image>().sprite = iconLib[ut.IconID]; 
			i++;
		}
		
		        
		for (int j = 0; j < 20; j++)
		{
			var islot = group.transform.GetChild(j);
			var button = islot.Find("ItemButton");
			int temp = j ;
			button.GetComponent<Button>().onClick.RemoveAllListeners();
            
			button.GetComponent<Button>().onClick.AddListener(delegate
			{
				var unit =  Player.Instance.UnitTemplates[temp];
				Debug.Log("true");
				PutInEmpty(unit);
		
			});
		}
		
		
		this.gameObject.SetActive(true);
	}


	private void PutInEmpty(UnitTemplate unit)
	{
		int i = 0;
		foreach (Button butt in partyButtons)
		{
			
			Image image = butt.transform.Find("Icon").gameObject.GetComponent<Image>();
			if (image.enabled == false)
			{
				if (contains(unit)) return;
				image.GetComponent<Image>().sprite = iconLib[unit.IconID]; 
				image.enabled = true;
				party[i] = unit;
				return;
			}

			i++;
		}
	}

	private bool contains(UnitTemplate unit)
	{
		foreach (UnitTemplate ut in party)
		{
			if (unit == ut) return true;
		}

		return false;
	}

	public void Deselect(int i)
	{
		partyButtons[i].transform.Find("Icon").GetComponent<Image>().enabled = false;
		party[i] = null;
		
	}

	public void ExtractParty()
	{
		Player.Instance.selectedParty = party;
		
	}
	
}
