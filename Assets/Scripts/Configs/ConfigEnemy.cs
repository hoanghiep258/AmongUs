using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigEnemyData
{
    public int id;
    public string name;
    public int hp;
    public int damage;
    public string namePrefab;
}
public class ConfigEnemy : ConfigDataTable<ConfigEnemyData>
{
    public ConfigEnemyData GetConfigEnemyByID(int id)
    {
        ConfigEnemyData cf=null;
        foreach(ConfigEnemyData e in records)
        {
            if(e.id==id)
            {
                cf = e;
                break;
            }
        }
        return cf;
    }
}
