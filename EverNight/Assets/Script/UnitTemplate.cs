using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementType
{
    NORMAL=0 , FIRE=1, EARTH=2, WATER=3, DARK=4, LIGHT=5 
}

[CreateAssetMenu(menuName = "Units/unit")]
[System.Serializable()]
public class UnitTemplate : ScriptableObject
{
    public string UnitName;
    public float HP;
    public float currentHP;
    public int IconID;
    
    public float HP_MIN;
    public float HP_MAX;
    public ElementType elementType;
    public List<float> Amplifier;
    public List<SpellBase> spells;
    public int level = 1;
    public void CreateUnit()
    {
        HP = Random.Range(HP_MIN, HP_MAX);
        //Player.Instance.unitList.Add(this);
        currentHP = HP;
    }


    public string ToString()
    {
        return "(name : "  + UnitName + " HP : " + HP + " Level " + level + ")";
    }
}

public class UnitChild : ScriptableObject
{
    public string sign = "fucker";
}
