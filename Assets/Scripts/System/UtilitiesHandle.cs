using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilitiesHandle
{
    public static List<T> GetListByString<T>(this string data, char key)
    {
        string[] s = data.Split(key);
        List<T> lst = new List<T>();
        for(int i = 0; i<s.Length; i++)
        {
            s[i] = s[i].Trim();
            T temp = (T)Convert.ChangeType(s[i], typeof(T));
            lst.Add(temp);
        }
        return null;
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }

}
