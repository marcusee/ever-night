using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    public bool isTurn = false;
    public bool wasTurnPrev = false;
    [HideInInspector]
    public turnSystemScript09 turnSystem;
    // Use this for initialization
    public int curr_ap = 1;
    public int ap = 1;

    public bool stun = false;
    public bool stopping = false;
    
    public void endMove()
    {
        
        curr_ap--;

        if (curr_ap <= 0)
        {
            isTurn = false;
            wasTurnPrev = true;
            turnSystem.EndUnitTurn();
        }
}

    
    public virtual void StartTurn(){}
}
