using System;

public interface ISaveLoadItem
{
    event Action<string, Action<int>> OnLoadDataNeeded;
    event Action<string, int> OnSaveDataNeeded;
}
