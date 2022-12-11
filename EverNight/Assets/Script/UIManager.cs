using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public DebugUI debugUI;

    public void PopMessage(string message)
    {
        if (debugUI == null)
            debugUI = GameObject.Find("DebugUI").GetComponent<DebugUI>();

        debugUI.ShowMessage(message);
    }


    public void showConfirmMessage()
    {
        
                
        
    }
}
