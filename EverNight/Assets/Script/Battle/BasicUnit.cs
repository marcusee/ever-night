using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class BasicUnit : MonoBehaviour {

    public UnitTemplate template;

    
    [HideInInspector]
    public bool isDead;

    [HideInInspector]
    public GameObject unitCanvas;

    public EffectSystem es;
    
    // Use this for initialization
    void Start()
    {
        template = Instantiate<UnitTemplate>(template);
        
        if(GetComponent<Enemy>()!= null)
            template.CreateUnit();
        
        isDead = false;
        unitCanvas = transform.Find("Canvas").gameObject;
        
        es = transform.Find("EffectSystem").gameObject.GetComponent<EffectSystem>();

        
        Quaternion camrot = Camera.main.transform.rotation;
        unitCanvas.transform.rotation = camrot;


        for (int i = 0; i < template.spells.Count; i++)
        {
            template.spells[i] =  Instantiate<SpellBase>(template.spells[i]);
            
            
            if(template.spells[i] .behaviour_type == 0 )
                template.spells[i].Initiate(template , new MeleeBehaviour());
            
            if(template.spells[i] .behaviour_type == 1 )
                template.spells[i].Initiate(template , new ProjectileBehaviour());
            
            if(template.spells[i] .behaviour_type == 2 )
                template.spells[i].Initiate(template , new StunBehaviour());
            
            if(template.spells[i] .behaviour_type == 3 )
                template.spells[i].Initiate(template , new AoeBehaviour());

            
        }

        unitCanvas.transform.Find("level").gameObject.GetComponent<Text>().text = ((int) template.currentHP).ToString();
        unitCanvas.transform.Find("level").gameObject.GetComponent<Text>().color = Color.blue;
    }

    private void Awake()
    {
        Start();
    }

    public void StopEffect()
    {
        if (es == null) return;
        es.StopAll();
    }
    
    public void StartUnit()
    {
        foreach (var spell in template.spells)
        {
            if (spell.current_cooldown > 0)
                spell.current_cooldown--;
        }
    }

    public void Heal(SpellBase sepll)
    {
        
                
    }
    
    public void TakeDamage(SpellBase spell)
    {

        var shake = GetComponent<ShakeComponent>();
        
        if (shake != null)
        {
            shake.StartShake();

        }
        
        float damage = template.Amplifier[(int)spell.type] * (spell.baseDamage * (spell.unitLevel * 1.25f) );
        template.currentHP -= damage;
        showDamageText((int) damage);
        
        AudioManager.Instance.PlayMelee();
        
        if(es != null)
            es.PlayEffect(spell.targetEffectID);
        
        if (template.currentHP < 1)
        {
            Dead();
        }
        
        unitCanvas.transform.Find("level").gameObject.GetComponent<Text>().text = ((int) template.currentHP).ToString();

    }

    public SpellBase CastSpell(int index)
    {
        return template.spells[index];
    }

    public void Dead()
    {
        isDead = true;
    }


    private void dying()
    {
        GameObject instance = Instantiate(transform.gameObject, 
            transform.position , Quaternion.identity) as GameObject;
        
    }
    
    public void Destroy()
    {
        Destroy(this.transform.gameObject);
    }

    private void showDamageText(int damage)
    {
        GameObject instance = Instantiate(Resources.Load("UI/DamageText" ), 
            unitCanvas.transform.position , Quaternion.identity) as GameObject;
        
        instance.transform.SetParent(unitCanvas.transform);
        instance.GetComponent<Text>().text = damage.ToString();
        
        Quaternion camrot = Camera.main.transform.rotation;
        instance.transform.rotation = camrot;
    }
}

