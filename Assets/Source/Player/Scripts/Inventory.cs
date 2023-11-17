using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static event Action<int> MushroomsAmountChanged;
    public static event Action<int> EggsAmountChanged;
    public static event Action<int> LegsAmountChanged;

    private int _mushroomsCount;
    private int _eggsCount;
    private int _legsCount;

    private void OnEnable()
    {
        Mushroom.MushroomCollected += ChangeMushroomsAmount;
        Egg.EggCollected += ChangeEggsAmount;
    }

    private void OnDisable()
    {
        Mushroom.MushroomCollected -= ChangeMushroomsAmount;
        Egg.EggCollected -= ChangeEggsAmount;
    }

    public void DeleteResources(int mushrooms, int eggs, int legs) 
    {
        ChangeMushroomsAmount(-mushrooms);
        ChangeEggsAmount(-eggs);
        ChangeLegsAmount(-legs);
    }

    private void ChangeMushroomsAmount(int amount)
    {
        _mushroomsCount += amount;
        MushroomsAmountChanged?.Invoke(_mushroomsCount);
    }

    private void ChangeEggsAmount(int amount)
    {
        _eggsCount += amount;
        EggsAmountChanged?.Invoke(_eggsCount);
    }

    private void ChangeLegsAmount(int amount)
    {
        _legsCount += amount;
        LegsAmountChanged?.Invoke(_legsCount);
    }
}
