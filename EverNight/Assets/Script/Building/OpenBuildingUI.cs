using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenBuildingUI : MonoBehaviour {

    public GameObject myobject;
    
    private void OnMouseDown()
    {
        // changes here
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            myobject.SetActive(true);

        }
    }
}
