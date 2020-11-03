using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Events;

public class DataEventTrigger : UnityEvent<object>
{

}

public static class DataTrigger
{
    public static Dictionary<string, DataEventTrigger> dicOnValueChange = new Dictionary<string, DataEventTrigger>();
    public static void RegisterValueChange(string s, UnityAction<object> delegateDataChange)
    {
        if (dicOnValueChange.ContainsKey(s))
            dicOnValueChange[s].AddListener(delegateDataChange);
        else
        {
            dicOnValueChange.Add(s, new DataEventTrigger());
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
    }

    public static void TriggerEventData(this object data, string path)
    {
        dicOnValueChange[path].Invoke(data);
    }
}

public class DatabaseLocal : MonoBehaviour
{
    private PlayerData playerData;

    #region Read
    public object ReadData(string path)
    {
        LoadData();
        string[] paths = SplitDataPath(path);
        if (paths.Length > 1)
        {
            List<string> lsPath = new List<string>();
            lsPath.AddRange(paths);
            lsPath.RemoveAt(0);
            object p = (object)playerData;
            object dataGet = null;
            RedDataByPath(p, out dataGet, lsPath);
            return dataGet;
        }
        else return playerData;
    }

    private void RedDataByPath(object p, out object dataGet, List<string> lsPath)
    {
        Type t = p.GetType();
        FieldInfo fieldInfo = t.GetField(lsPath[0]);
        if (lsPath.Count == 1)
            dataGet = fieldInfo.GetValue(p);
        else
        {
            lsPath.RemoveAt(0);
            RedDataByPath(fieldInfo.GetValue(p), out dataGet, lsPath);
        }
    }
    #endregion

    #region Insert
    public void InsertData<T>(string path, object data, Action callback = null)
    {
        string[] paths = SplitDataPath(path);
        if (paths.Length > 1)
        {
            List<string> lsPath = new List<string>();
            lsPath.AddRange(paths);
            lsPath.RemoveAt(0);
            object p = (object)playerData;
            InsetDataByPath<T>(p, data, lsPath, callback);
        }
        else playerData = (PlayerData)data;

        SaveData();
    }

    private void InsetDataByPath<T>(object p, object data, List<string> lsPath, Action callback)
    {
        Type t = p.GetType();
        FieldInfo fieldInfo = t.GetField(lsPath[0]);
        if(lsPath.Count == 1)
        {
            object valueData = fieldInfo.GetValue(p);
            List<T> ls = (List<T>)valueData;

            ls.Add((T)data);
            fieldInfo.SetValue(p, ls);
            if (callback != null)
                callback();
        }
        else
        {
            lsPath.RemoveAt(0);
            InsetDataByPath<T>((object)fieldInfo.GetValue(p), data, lsPath, callback);
        }

        SaveData();
    }
    #endregion

    #region Update
    public void UpdateData<T>(string path, object data, Action callback = null, bool isArray = false)
    {
        string[] paths = SplitDataPath(path);
        if (paths.Length > 1)
        {
            List<string> lsPath = new List<string>();
            lsPath.AddRange(paths);
            lsPath.RemoveAt(0);
            object p = (object)playerData;

            if (!isArray)
                UpdateDataByPath<T>(p, data, lsPath);
            else
                UpdateDataByPathArray<T>(p, data, lsPath);
        }
        else playerData = (PlayerData)data;

        SaveData();
    }

    private void UpdateDataByPathArray<T>(object p, object data, List<string> lsPath)
    {
        Type t = p.GetType();
        FieldInfo fieldInfo = t.GetField(lsPath[0]);
        if(lsPath.Count == 2)
        {
            int index = int.Parse(lsPath[1]);
            object valueData = fieldInfo.GetValue(p);
            List<T> ls = (List<T>)valueData;

            for(int i = 0; i<ls.Count; i++)
            {
                if (i == index)
                    ls[i] = (T)data;
            }
            fieldInfo.SetValue(p, ls);
        }
        else
        {
            lsPath.RemoveAt(0);
            UpdateDataByPathArray<T>((object)fieldInfo.GetValue(p), data, lsPath);
        }

        SaveData();
    }

    private void UpdateDataByPath<T>(object p, object data, List<string> lsPath)
    {
        Type t = p.GetType();
        FieldInfo fieldInfo = t.GetField(lsPath[0]);
        if (lsPath.Count == 1)
        {
            fieldInfo.SetValue(p, data);
        }
        else
        {
            lsPath.RemoveAt(0);
            UpdateDataByPath<T>((object)fieldInfo.GetValue(p), data, lsPath);
        }

        SaveData();
    }
    #endregion

    private string[] SplitDataPath(string path)
    {
        return path.Split('/');
    }

    private void LoadData()
    {
        playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("Data"));
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("Data", JsonUtility.ToJson(playerData));
    }
}
