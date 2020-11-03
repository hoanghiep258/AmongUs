using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigHeroData
{
    public int id;
    public string name;
    public int hp;
    public int damage;
    public string namePrefab;
    public float rof;
    public float rangeDetect;
    public float rangeAttack;
}

public class ConfigHero : ConfigDataTable<ConfigHeroData>
{
    public override void AddKeySort()
    {
        OnAddKeySort(new ConfigComparePrimaryKey<ConfigHeroData>("id"));
    }

    public ConfigHeroData GetConfigHeroDataByID(int idHero)
    {
        ConfigHeroData configHeroData = null;
        foreach (ConfigHeroData e in records)
        {
            if (e.id == idHero)
            {
                configHeroData = e;
                break;
            }
        }

        return configHeroData;
    }
}
