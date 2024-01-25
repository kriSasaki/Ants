using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStorageService
{
    void Save(string key, int data, Action<bool> callback = null);
    void Load(string key, Action<int> callback);
}
