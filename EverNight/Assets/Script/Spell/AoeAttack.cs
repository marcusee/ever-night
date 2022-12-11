using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spell/AOEAttack")]
public class AoeAttack : SpellBase
{

	public override void Activate(SpellSystem spellSystem)
	{
		current_cooldown = cooldown;

		var current = spellSystem.turnSystem.currentOBJ;

		var piece = current.GetComponent<Piece>();

		spellSystem.turnSystem.ApplyDamageToTeam();
        
		spellSystem.turnSystem.currentOBJ.GetComponent<Piece>().endMove();
		
		
		

	}

}
