using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<int> MushroomsAmountChanged;
    public event Action<int> EggsAmountChanged;

    public int MushroomsCount {  get; private set; }
    public int EggsCount { get; private set; }

    public void DeleteResources(int mushrooms, int eggs, int legs) 
    {
        ChangeMushroomsAmount(-mushrooms);
        ChangeEggsAmount(-eggs);
    }

    public void ChangeMushroomsAmount(int amount)
    {
        MushroomsCount += amount;
        MushroomsAmountChanged?.Invoke(MushroomsCount);
    }

    public void ChangeEggsAmount(int amount)
    {
        EggsCount += amount;
        EggsAmountChanged?.Invoke(EggsCount);
    }
}
