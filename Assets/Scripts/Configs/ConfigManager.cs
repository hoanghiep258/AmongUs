using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : SingletonMono<ConfigManager>
{
    public static ConfigEnemy configEnemy;
    public void InitConfig(Action callback)
    {
        StartCoroutine(LoopLoadConfig(callback));
    }

    IEnumerator LoopLoadConfig(Action callback)
    {
        yield return new WaitForSeconds(0.1f);
        configEnemy = Resources.Load("Configs/ConfigEnemy", typeof(ScriptableObject)) as ConfigEnemy;
        while (configEnemy == null)
        {
            yield return null;
        }
        if (callback != null)
            callback();
    }
}
