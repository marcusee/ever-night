using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystem : MonoBehaviour {
    [HideInInspector]
    public turnSystemScript09 turnSystem;

    [HideInInspector]
    public int selectedSpell;

    [HideInInspector]
    public MeleeSystem meleeSystem;

	// Use this for initialization
	void Start () {
        turnSystem = GameObject.Find("Turn-basedSystem").GetComponent<turnSystemScript09>();
	    meleeSystem = GetComponent<MeleeSystem>();
//	    EffectSystem.Instance.Initialized();
	}

    // Update is called once per frame
    void Update () {
		
	}

    public void SetSpell(int index)
    {
        selectedSpell = index;
    }


    public void StartProjectileSpell(SpellBase spell , string projectileName)
    {
        turnSystem.state = STATE.ANIMATING;

        GameObject instance = Instantiate(Resources.Load("Projectile/" + projectileName), 
            turnSystem.currentOBJ.transform.position , Quaternion.identity) as GameObject;


        if (instance == null) Debug.Log("nothing");

        instance.GetComponent<Projectile>()
            .target = turnSystem.targetOBJ.transform;

        StartCoroutine("WaitProjectileFinish" , spell);
    }

    IEnumerator WaitProjectileFinish(SpellBase spell)
    {
    
        yield return  new WaitForSeconds(.3f);
        turnSystem.ApplyDamageToTarget(spell);
        spell.EffectUnit(turnSystem.targetOBJ);
        turnSystem.EndCurrentTurn();
        turnSystem.state = STATE.WAITING;

    }

    public void StartAOESpell()
    {
       // turnSystem.CastSpellOnTeam();
        
        
    }

    public void LaunchFromTop(SpellBase spell,string projectileName)
    {
        turnSystem.state = STATE.ANIMATING;
        var start = turnSystem.targetOBJ.transform.position;
        start = start + Vector3.up * 10;
        GameObject instance = Instantiate(Resources.Load("Projectile/" + projectileName), 
            start , Quaternion.identity) as GameObject;
        
        if (instance == null) Debug.Log("nothing");

        instance.GetComponent<Projectile>().target = turnSystem.targetOBJ.transform;

        StartCoroutine("WaitProjectileFinish" , spell);
        
    }

    public void StartMeleeAction()
    {
        meleeSystem.StartMelee(turnSystem.currentOBJ , turnSystem.targetOBJ);
    }
    
    
}
