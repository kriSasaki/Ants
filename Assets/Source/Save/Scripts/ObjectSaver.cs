using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSaver : IStorageService
{
    public void Save(string key, int data, Action<bool> callback = null)
    {
        PlayerPrefs.SetInt(key, data);
        callback?.Invoke(true);
    }

    public void Load(string key, Action<int> callback)
    {
        callback?.Invoke(PlayerPrefs.GetInt(key));
    }
}
