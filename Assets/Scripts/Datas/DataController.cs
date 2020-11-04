using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DataController : MonoBehaviour
{
    [SerializeField]
    private DatabaseLocal databaseLocal;
    private PlayerData playerData;

    public void InitData()
    {
        if (databaseLocal.ReadData(PathData.root_db) != null)
        { 
            playerData = (PlayerData)databaseLocal.ReadData(PathData.root_db);
        }
        else
        {
            databaseLocal.InsertData<PlayerData>(PathData.root_db, new PlayerData());
        }
    }

    public PlayerInfo GetPlayerInfo()
    {
        return (PlayerInfo)databaseLocal.ReadData(PathData.playerInfo);
    }
    
    public void SetName(string newName)
    {
        string currentName = GetName();
        currentName = newName;

        databaseLocal.UpdateData<string>(PathData.name, currentName);
    }

    public string GetName()
    {
        return (string)databaseLocal.ReadData(PathData.name);
    }

    public void SetCoin(int curCoin)
    {
        databaseLocal.UpdateData<int>(PathData.name, curCoin);
    }

    public int GetCoin()
    {
        return (int)databaseLocal.ReadData(PathData.coin);
    }

    public void SetHat(int index)
    {
        databaseLocal.UpdateData<string>(PathData.hat, index);
    }

    public int GetHat()
    {
        return (int)databaseLocal.ReadData(PathData.hat);
    }


    public void SetColor(int index)
    {
        databaseLocal.UpdateData<string>(PathData.color, index);
    }

    public int GetColor()
    {
        return (int)databaseLocal.ReadData(PathData.color);
    }

    public void SetSkin(int index)
    {
        databaseLocal.UpdateData<string>(PathData.skin, index);
    }

    public int GetSkin()
    {
        return (int)databaseLocal.ReadData(PathData.skin);
    }


    public void SetPet(int index)
    {
        databaseLocal.UpdateData<string>(PathData.pet, index);
    }

    public int GetPet()
    {
        return (int)databaseLocal.ReadData(PathData.pet);
    }

    public void UpdateLsBoughtColor(string content)
    {
        databaseLocal.UpdateData<string>(PathData.lsBoughtColor, content);
    }


    public void UpdateLsBoughtHat(string content)
    {
        databaseLocal.UpdateData<string>(PathData.lsBoughtHat, content);
    }


    public void UpdateLsBoughtSkin(string content)
    {
        databaseLocal.UpdateData<string>(PathData.lsBoughtSkin, content);
    }


    public void UpdateLsBoughtPet(string content)
    {
        databaseLocal.UpdateData<string>(PathData.lsBoughtPet, content);
    }

}

#if UNITY_EDITOR
public static class DataLoacalTool
{
    [MenuItem("Tools/Data/Reset Local Data", false, 1)]
    private static void ResetLocalData()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Tools/Data/Get Local Data", false, 1)]
    private static void GetLocalData()
    {
        string data = PlayerPrefs.GetString("Data");
        Debug.LogError(data.ToString());
    }
}
#endif
