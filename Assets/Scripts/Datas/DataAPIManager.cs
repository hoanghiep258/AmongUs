using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataAPIManager : SingletonMono<DataAPIManager>
{
    [SerializeField]
    private DataController dataController; 

    public void InitData(Action callback)
    {
        dataController.InitData();
        if (callback != null)
            callback();
    }

    public void SetName(string newName)
    {
        dataController.SetName(newName);
    }

    public string GetName()
    {
        return dataController.GetName();
    }

    public void SetColor(int value)
    {
        dataController.SetColor(value);
    }

    public int GetColor()
    {
        return dataController.GetColor();
    }

    public void SetHat(int value)
    {
        dataController.SetHat(value);
    }

    public int GetHat()
    {
        return dataController.GetHat();
    }


    public void SetSkin(int value)
    {
        dataController.SetSkin(value);
    }

    public int GetSkin()
    {
        return dataController.GetSkin();
    }


    public void SetPet(int value)
    {
        dataController.SetPet(value);
    }

    public int GetPet()
    {
        return dataController.GetPet();
    }
}

