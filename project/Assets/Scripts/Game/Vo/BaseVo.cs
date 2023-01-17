using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVo<T>
{
    private static Dictionary<int, T> VoMap = new Dictionary<int, T>();
    private static List<T> VoList = new List<T>();
    // single
    public static T single
    {
        get
        {
            return list[0];
        }
    }
    // list
    public static List<T> list
    {
        get
        {
            return VoList;
        }
    }

    // add
    public static void AddVo(int id, T Vo)
    {
        if (VoMap.TryAdd(id, Vo))
        {
            VoList.Add(Vo);
        }
    }
    // remove
    public static void RemoveVo(int id)
    {
        if (VoMap.Remove(id))
        {
            VoList.Remove(VoMap[id]);
        }
    }
    // clear
    public static void Clear()
    {
        VoMap.Clear();
        VoList.Clear();
    }
    // get
    public static T GetVoById(int id)
    {
        T t;
        if (VoMap.TryGetValue(id, out t))
        {
            return t;
        }
        return default(T);
    }
}