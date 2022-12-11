using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class specialShopScript : MonoBehaviour {

    public DebugUI ui;

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
            if (Input.GetMouseButtonDown(0))
            {
                //Summon();
                //IncreaseLevel();
                ui.transform.Find("inAppPurchaseUI").gameObject.SetActive(true);

                /*

                */
            }
            else if (Input.GetMouseButton(1))
            {

            }
        }
    }
}
