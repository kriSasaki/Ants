using System;

public interface IStorageService
{
    void Save(string key, int data, Action<bool> callback = null);
    void Load(string key, Action<int> callback);
}
