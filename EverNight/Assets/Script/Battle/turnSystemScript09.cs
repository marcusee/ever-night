using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum STATE
{
    WAITING,ENEMY_TURN ,DECIDING,ANIMATING
}

public class turnSystemScript09 : MonoBehaviour {
    public BattleUI battleUI;
    public List<Piece> playersGroup;
    public GameObject currentOBJ;
    public GameObject targetOBJ;
    public STATE state = STATE.DECIDING;
    private SpellSystem spellSystem;
    private bool turnJustEnded = false;

    private int turnCount = 0;
    public BattleManager bm;
    private bool fightEnd = false;
    // Use this for initialization
    void Start () {

    }

    void Awake()
    {

    }
    void OnGUI()
    {
        /*
        if(currentOBJ !=null)
            GUI.Label(new Rect(10, 10, 250, 400 ) ,((int) currentOBJ.GetComponent<BasicUnit>().template.currentHP).ToString());

        */
    }
	// Update is called once per frame
	void Update () {
	    if (fightEnd)
	    {
	        
	    }
	    else
	    {
	        UpdateTurns();
	    }
	}



    public void ApplyDamageToTarget(SpellBase spell)
    {
        targetOBJ.GetComponent<BasicUnit>().TakeDamage(spell);
    }
    
    public void ApplyDamageToTarget()
    {
        if (targetOBJ == null) return;
        targetOBJ.GetComponent<BasicUnit>().TakeDamage(currentOBJ.GetComponent<BasicUnit>().CastSpell(spellSystem.selectedSpell));
    }

    public void ApplyDamageToTarget(GameObject target)
    {
        target.GetComponent<BasicUnit>().TakeDamage(currentOBJ.GetComponent<BasicUnit>().CastSpell(spellSystem.selectedSpell));
    }
    
    public void ApplyDamageToTeam()
    {
        var bu = targetOBJ.GetComponent<Piece>();
        List<GameObject> targeted_team = new List<GameObject>();
        if (bu is PlayerMove)
        {
            targeted_team = getPlayers();
        }
        else
        {
            targeted_team = getEnemies();
            
        }

        foreach (GameObject go in targeted_team)
        {
            ApplyDamageToTarget(go);
            
        }
    }

    
    public void EndCurrentTurn()
    {
        currentOBJ.GetComponent<Piece>().endMove();
    }

    public void CastRandomSpell()
    {
        int max = currentOBJ.GetComponent<BasicUnit>().template.spells.Count;
        int chance = Random.Range(0, max );
        
        spellSystem.selectedSpell = chance;
    
        CastSpellOnTarget();

        /*
        var spell = currentOBJ.GetComponent<BasicUnit>().CastSpell(chance);
        targetOBJ.GetComponent<BasicUnit>().TakeDamage(spell);
        currentOBJ.GetComponent<Piece>().endMove();
*/
    }
    
    public void CastSpellOnTarget(GameObject target)
    {
        
        int index = spellSystem.selectedSpell;
        if (currentOBJ == null || target == null) return;
        var spell = currentOBJ.GetComponent<BasicUnit>().CastSpell(index);
        spell.Activate(spellSystem);
    }
    
    public void CastSpellOnTarget()
    {
        CastSpellOnTarget(targetOBJ);

    }
    public void CastSpellOnTeam()
    {
        int index = spellSystem.selectedSpell;
        //int index = 0;
      //  if (currentOBJ == null || targetOBJ == null) return;
        var spell =
            currentOBJ.GetComponent<BasicUnit>().CastSpell(index);
        
        spell.Activate(spellSystem);

    }
    public List<GameObject> getPlayers()
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < playersGroup.Count; i++)
        {
            if (playersGroup[i].GetComponent<PlayerMove>() != null)
            {
                list.Add(playersGroup[i].transform.gameObject);
            }
        }
        return list;
    }

    public List<GameObject> getEnemies()
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < playersGroup.Count; i++)
        {
            if (playersGroup[i].GetComponent<Enemy>() != null)
            {
                list.Add(playersGroup[i].transform.gameObject);
            }
        }
        return list;
    }

    public GameObject GetRandomPlayer()
    {
        List<GameObject> list = getPlayers();
        return list[Random.Range(0, list.Count)];
    }

    void ResetTurns()
    {
        
        for(int i = 0; i < playersGroup.Count; i++)
        {
            playersGroup[i].curr_ap = playersGroup[i].ap;

            if(i == 0)
            {
                playersGroup[i].isTurn = true;
                playersGroup[i].wasTurnPrev = false;

            }
            else
            {
                playersGroup[i].isTurn = false;
                playersGroup[i].wasTurnPrev = false;
            }
        }
    }

    void UpdateTurns()
    {
        for (int i = 0; i < playersGroup.Count; i++)
        {
            if (playersGroup[i] == null)
            {

                continue;

            }
            if (!playersGroup[i].wasTurnPrev)
            {
                
                if (turnJustEnded)
                {

                    if (playersGroup[i].stopping)
                    {
                        playersGroup[i].transform.gameObject.GetComponent<BasicUnit>().StopEffect();
                    }

                    if (playersGroup[i].stun)
                    {

                        playersGroup[i].wasTurnPrev = true;
                        playersGroup[i].stun = false;
                        playersGroup[i].stopping = true;

                        continue;
                        
                    }
                    
                    StartTurn(i);
                }
                
                playersGroup[i].isTurn = true;


                
                break;
            }
            /// last guy reset everybody after everybody turn
             if (i == playersGroup.Count - 1 && playersGroup[i].wasTurnPrev) {
                ResetTurns();
            }
        }
    }

    public void SetTarget(GameObject obj)
    {
        // if is enemy
        if(obj.GetComponent<Enemy>() != null)
        {
            var list = getEnemies();
            foreach(GameObject o in list)
            {
                battleUI.deactivateSelector(o);
            }

            battleUI.activateSelector(obj);
            targetOBJ = obj;
        }
    }



    public void EndUnitTurn()
    {
        turnJustEnded = true;
        handleDead();
        deactiveAllSelector();
        
        handleWin();
        handleLose();
    }

    void deactiveAllSelector()
    {
        for (int i = 0; i < playersGroup.Count; i++)
        {
            battleUI.deactivateSelector(playersGroup[i].transform.gameObject);
        }
    }

    private void handleDead()
    {
        for (int i = 0; i < playersGroup.Count; )
        {
            if (playersGroup[i] == null) playersGroup.RemoveAt(i);
            var bu = playersGroup[i].transform.gameObject.GetComponent<BasicUnit>();

            if (bu.isDead)
            {
                bu.Destroy();
                playersGroup.RemoveAt(i);
                continue;

            }
            else i++;
        }
        
        
    }

    private void handleLose()
    {
        if (getPlayers().Count <= 0)
        {
            battleUI.transform.Find("DefeatedPanel").gameObject.SetActive(true);
            endFight();

        }
    }

    private void handleWin()
    {
        if (getEnemies().Count <= 0)
        {
            battleUI.transform.Find("VictoryPanel").gameObject.SetActive(true);
            Player.Instance.money += 100;
            endFight();
        }
    }

    public void endFight()
    {
        fightEnd = true;

        foreach (var p in playersGroup)
        {
            Destroy(p.transform.gameObject);
        }


        if (bm.boss && getEnemies().Count <=0 )
        {
            battleUI.transform.Find("BossPanel").gameObject.SetActive(true);
        }

        bm.boss = false;

    }
    
    public void StartFight()
    {
        fightEnd = false;
        spellSystem = GetComponent<SpellSystem>();
        ResetTurns();
        EndUnitTurn();
        StartTurn(0);
        turnJustEnded = false;
    }
    
    
    /// <summary>
    /// initilize logic once at a system level . individual units have a startTurn too but it is at a unit level.
    /// </summary>
    /// <param name="i"> index of uint in the list</param>
    private void StartTurn(int i)
    {
        
        targetOBJ = null;
        currentOBJ = playersGroup[i].transform.gameObject;
        spellSystem.selectedSpell = 0;
        
        var isAPlayer = currentOBJ.GetComponent<PlayerMove>();
        if (isAPlayer != null) // check if is a player unit
        {
            if ( turnCount != 0&& getEnemies().Count == 0)
            {
                //SceneManager.LoadScene("homescene", LoadSceneMode.Single);
                
            }
            
            var currentUnitTurn = playersGroup[i].transform.gameObject;
            updateButtonUI(currentUnitTurn.GetComponent<BasicUnit>().template);
            battleUI.activateSelector(currentUnitTurn);
        }
    
        // this method do a logic once when it is Unit turn.
        playersGroup[i].StartTurn();
        currentOBJ.GetComponent<BasicUnit>().StartUnit();

        // control varible to make start turn run  once only
        turnJustEnded = false;
    }

    private void updateButtonUI(UnitTemplate unit)
    {
        battleUI.UpdateUI(unit);
    }

    
}


