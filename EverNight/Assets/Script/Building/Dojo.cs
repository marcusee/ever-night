using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dojo : MonoBehaviour
{
	public GameObject unitList;

	public DebugUI ui;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnMouseDown()
	{
		if (!IsPointerOverUIObject())
		{
			ui.SetUpUnitList();
			unitList.SetActive(true);
			
		}
	}

	public void levelup()
	{
		if (ui.selectedUnit == null) return;

		if (Player.Instance.money >= 1000 * ui.selectedUnit.level)
		{
			ui.selectedUnit.level++;
			Player.Instance.money -= 1000 * ui.selectedUnit.level;
			//unitList.SetActive(false);
			ui.SetUpUnitList();



		}
		
	}
	
		
	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	
}
