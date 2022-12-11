using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Piece {

	// Use this for initialization
	void Start () {
	    turnSystem = GameObject.Find("Turn-basedSystem").GetComponent<turnSystemScript09>();
	}

    void Awake()
    {
        Start();

    }
    
    public override void StartTurn()
    {
        base.StartTurn();
        if (turnSystem == null) return;
        
        turnSystem.currentOBJ = 
            this.transform.gameObject;
        
        turnSystem.state = STATE.DECIDING;
    }
    
    // Update is called once per frame
    void Update() {
        
        if (isTurn)
        {
            if (turnSystem.state == STATE.WAITING)
            {
                
                turnSystem.state = STATE.DECIDING;
            }        
        }
        
	}

 

    public void OnMouseDown()
    {
        if(turnSystem.state == STATE.DECIDING)
        {
        }
    }
}
