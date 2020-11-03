using System;
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
    public int color;
    public int hat;
    public int skin;
    public int pet;

    public PlayerInfo()
    {
        this.name = "Player";
        this.color = 0;
        this.hat = 0;
        this.pet = 0;
        this.skin = 0;
    }
}
