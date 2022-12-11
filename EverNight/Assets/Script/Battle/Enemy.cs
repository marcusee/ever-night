using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Piece {

   // public turnSystemScript09 turnSystem;
    //bool isTurn = false;
    // Use this for initialization
    void Start()
    {
        turnSystem = GameObject.Find("Turn-basedSystem").GetComponent<turnSystemScript09>();
    }


    public override void StartTurn()
    {
     
        base.StartTurn();
        if (turnSystem == null) return;
        turnSystem.currentOBJ = this.transform.gameObject;
        turnSystem.state = STATE.ENEMY_TURN;
        StartCoroutine("StartRandomAttack");
    }

    // Update is called once per frame
    void Update () {

        if (isTurn)
        {
            if (turnSystem.state == STATE.WAITING)
            {
                turnSystem.state = STATE.ENEMY_TURN;
            }
        }

    }

    IEnumerator StartRandomAttack()
    {
        yield return new WaitForSeconds(0.5f);
        RandomAttack();
        StopCoroutine("StartRandomAttack");
    }

    void RandomAttack()
    {
        turnSystem.targetOBJ = turnSystem.GetRandomPlayer();
        turnSystem.CastRandomSpell();
    }

    
    public void OnMouseDown()
    {
        if (turnSystem.state == STATE.DECIDING)
        {
            turnSystem.SetTarget(this.transform.gameObject);
            turnSystem.CastSpellOnTarget();
        }
    }
}
