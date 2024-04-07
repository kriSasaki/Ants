using System;
using UnityEngine;

namespace Source.Save.Scripts
{
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
}
