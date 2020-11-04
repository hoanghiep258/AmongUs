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

    public void AddCoin(int valueCoin)
    {
        int curCoin = dataController.GetCoin();
        curCoin += valueCoin;
        dataController.SetCoin(curCoin);
    }

    public int GetCoin()
    {
        return dataController.GetCoin();
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

    public string[] GetLsBoughtColor()
    {
        string lsBoughtColor = dataController.GetPlayerInfo().lsBoughtColor;
        return lsBoughtColor.Split(',');
    }

    public void AddColor(string id)
    {
        string lsBoughtColor = dataController.GetPlayerInfo().lsBoughtColor;
        if (!lsBoughtColor.Contains(id))
        {
            lsBoughtColor += "-" + id;
            dataController.UpdateLsBoughtColor(lsBoughtColor);
        }        
    }

    public string[] GetLsBoughtHat()
    {
        string lsBoughtHat = dataController.GetPlayerInfo().lsBoughtHat;
        return lsBoughtHat.Split(',');
    }


    public void AddHat(string id)
    {
        string lsBoughtHat = dataController.GetPlayerInfo().lsBoughtHat;
        if (!lsBoughtHat.Contains(id))
        {
            lsBoughtHat += "-" + id;
            dataController.UpdateLsBoughtHat(lsBoughtHat);
        }
    }

    public string[] GetLsBoughtSkin()
    {
        string lsBoughtSkin = dataController.GetPlayerInfo().lsBoughtSkin;
        return lsBoughtSkin.Split(',');
    }

    public void AddSkin(string id)
    {
        string lsBoughtSkin = dataController.GetPlayerInfo().lsBoughtSkin;
        if (!lsBoughtSkin.Contains(id))
        {
            lsBoughtSkin += "-" + id;
            dataController.UpdateLsBoughtSkin(lsBoughtSkin);
        }
    }

    public string[] GetLsBoughtPet()
    {
        string lsBoughtPet = dataController.GetPlayerInfo().lsBoughtPet;
        return lsBoughtPet.Split(',');
    }

    public void AddPet(string id)
    {
        string lsBoughtPet = dataController.GetPlayerInfo().lsBoughtPet;
        if (!lsBoughtPet.Contains(id))
        {
            lsBoughtPet += "-" + id;
            dataController.UpdateLsBoughtPet(lsBoughtPet);
        }
    }
}

