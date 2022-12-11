using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Singleton<Player>
{
    protected Player() { } // guarantee this will be always a singleton only - can't use the constructor!


    public int level = 1;

    public int money = 10000;
    public int gems = 500;


    public List<UnitTemplate> UnitTemplates = new List<UnitTemplate>();

    public int UnitLoadedCount;

    public UnitTemplate[] selectedParty  = new UnitTemplate[3];
}
