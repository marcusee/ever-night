using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class SpellBehaviour
{

    public SpellBase sb;
    
    public virtual void Activate(SpellSystem spellSystem)
    {
        
    }
    
    public virtual void EffectUnit(GameObject unit)
    {
        
    }
}

public class ProjectileBehaviour : SpellBehaviour
{
    public override void Activate(SpellSystem spellSystem)
    {
        sb.current_cooldown = sb.cooldown;
        spellSystem.StartProjectileSpell(sb , sb.projectileName);
    }
    
    public override void EffectUnit(GameObject unit)
    {
        
    }
    
}

public class AoeBehaviour : SpellBehaviour
{
    public override void Activate(SpellSystem spellSystem)
    {
        sb.current_cooldown = sb.cooldown;

        var current = spellSystem.turnSystem.currentOBJ;

        var piece = current.GetComponent<Piece>();

        spellSystem.turnSystem.ApplyDamageToTeam();
        
        spellSystem.turnSystem.currentOBJ.GetComponent<Piece>().endMove();
    }
    
    public override void EffectUnit(GameObject unit)
    {
    }
}


public class StunBehaviour : SpellBehaviour
{
    public override void Activate(SpellSystem spellSystem)
    {
        sb.current_cooldown = sb.cooldown;
        spellSystem.LaunchFromTop(sb , sb.projectileName);
    }
    
    public override void EffectUnit(GameObject unit)
    {
        if (unit.GetComponent<Boss>() != null) return;
		
        unit.GetComponent<Piece>().stun = true;
		
    }
}

public class MeleeBehaviour : SpellBehaviour
{
    public override void Activate(SpellSystem spellSystem)
    {
        sb.current_cooldown = sb.cooldown;

        spellSystem.StartMeleeAction();
    }
    
    public override void EffectUnit(GameObject unit)
    {
        
    }
    
}

[CreateAssetMenu(menuName = "Spell/spells")]
public class SpellBase : ScriptableObject
{
    private SpellBehaviour sb;
    
    public int targetEffectID = -1;
    public int cooldown = 0;
    
    [HideInInspector]
    public int current_cooldown = 0;

    public int iconId = 0;
    public ElementType type;
    public float baseDamage;
    
    [HideInInspector]
    public int unitLevel = 1;

    public int behaviour_type;
    public string projectileName;
    
    public  virtual void Initiate(UnitTemplate unit , SpellBehaviour sb)
    {
        this.sb = sb;
        sb.sb = this;
        
        current_cooldown = 0;
        unitLevel = unit.level;
    }

    public virtual void Activate(SpellSystem spellSystem)
    {
        sb.Activate(spellSystem);
    }

    public virtual void EffectUnit(GameObject unit)
    {
        sb.EffectUnit(unit);
    }

}


[CreateAssetMenu(menuName = "Spell/MeleeAttack")]
public class MeleeAttack : SpellBase
{

    public override void Activate(SpellSystem spellSystem)
    {
        current_cooldown = cooldown;

        spellSystem.StartMeleeAction();
        
    }

}




