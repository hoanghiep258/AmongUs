﻿using System;
using System.Collections;
using System.Collections.Generic;

public class DataObject 
{
    public string uuid;

    public DataObject()
    {
        uuid = Guid.NewGuid().ToString();
    }
}

[Serializable]
public class PlayerData : DataObject
{
    public PlayerInfo playerInfo = new PlayerInfo();
}

[Serializable]
public class PlayerInfo
{
    public string name;
    public int coin;

    public int color;
    public int hat;
    public int skin;
    public int pet;

    public string lsBoughtColor;
    public string lsBoughtHat;
    public string lsBoughtSkin;
    public string lsBoughtPet;
    
    public PlayerInfo()
    {
        this.name = "Player";
        this.coin = 0;

        this.color = 0;
        this.hat = 0;
        this.pet = 0;
        this.skin = -1;

        this.lsBoughtColor = "0-1-2-3";
        this.lsBoughtHat = "0";
        this.lsBoughtSkin = "0";
        this.lsBoughtPet = "0";


    }
}
