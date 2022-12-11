using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int level;
    public int money;
    public int gems;
    public int UnitCount;

    public List<UnitSaveData> list = new List<UnitSaveData>();
}

[System.Serializable]
public class UnitSaveData
{
    public string name;
    public int level;
    public int HP;
}

[System.Serializable]
public class Building
{
    public int summonLevel;
    public int genMoneyLevel;
    public DateTime time;

}


public class SaveLoad : MonoBehaviour
{
    public Summoning summon;
    public genmoney genmoney;

    public UnitTemplate temp;

    public bool loadOnStart;
    
    // Use this for initialization
    void Start()
    {
        if (loadOnStart)
        {
            Load();
        }
        
        InvokeRepeating("Save", 5, 5f);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public  void Save()
    {
        SavePlayer();
        for (int i = 0; i < Player.Instance.UnitTemplates.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", "unit", i));
            var json = JsonUtility.ToJson(Player.Instance.UnitTemplates[i]);
            bf.Serialize(file, json);
            file.Close();
        }
        
        SaveBuilding();

    }

    public  void Load()
    {

        LoadPlayer();

        for (int i = 0; i < Player.Instance.UnitLoadedCount ; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", "unit", i)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", "unit", i), FileMode.Open);
                UnitTemplate unit_t = Instantiate<UnitTemplate>(temp);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), unit_t);
                Player.Instance.UnitTemplates.Add(unit_t);
                file.Close();
            }
        }
        
       LoadBuildings();

    }

    private  void SavePlayer()
    {
        SaveData data = new SaveData();
        data.gems = Player.Instance.gems;
        data.money = Player.Instance.money;
        data.UnitCount = Player.Instance.UnitTemplates.Count;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, data);
        file.Close();
    }

    private  void LoadPlayer()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            Player.Instance.gems = data.gems;
            Player.Instance.money = data.money;
            Player.Instance.UnitLoadedCount = data.UnitCount;
            file.Close();

            // delete all the units
            Player.Instance.UnitTemplates.Clear();
        }

    }

    private void SaveBuilding()
    {
        Building building = new Building();
        building.summonLevel = summon.buildingLevel;
        building.genMoneyLevel = genmoney.buildingLevel;
        building.time = genmoney.timeStamp;
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/building.gd");
        bf.Serialize(file, building);
        file.Close();
    }
    
    
    
    private void LoadBuildings()
    {

        
        if (File.Exists(Application.persistentDataPath + "/building.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/building.gd", FileMode.Open);
            Building data = (Building)bf.Deserialize(file);

            summon.Load(data.summonLevel);
            genmoney.Load(data.genMoneyLevel);
            genmoney.Load(data.genMoneyLevel);

            file.Close();

        }
    }
}