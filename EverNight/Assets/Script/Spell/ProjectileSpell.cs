using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Spell/projectileSpell")]

public class ProjectileSpell : SpellBase
{
    public override void Activate(SpellSystem spellSystem)
    {
        current_cooldown = cooldown;
        spellSystem.StartProjectileSpell(this , projectileName);
    }

}
