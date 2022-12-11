using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Stun")]
public class Stun : SpellBase
{

	public string projectileName;

    
	public override void Activate(SpellSystem spellSystem)
	{
		current_cooldown = cooldown;
		spellSystem.LaunchFromTop(this , projectileName);
	}
    
	public override void EffectUnit(GameObject unit)
	{
		if (unit.GetComponent<Boss>() != null) return;
		
		unit.GetComponent<Piece>().stun = true;
		
	}
	

}