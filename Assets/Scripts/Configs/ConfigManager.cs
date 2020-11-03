using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : SingletonMono<ConfigManager>
{

    public void InitConfig(Action callback)
    {
        StartCoroutine(LoopLoadConfig(callback));
    }

    IEnumerator LoopLoadConfig(Action callback)
    {
        yield return new WaitForSeconds(0.1f);

        if (callback != null)
            callback();
    }
}
