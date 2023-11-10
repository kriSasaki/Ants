using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static event Action<int> MushroomsAmountChanged;
    public static event Action<int> EggsAmountChanged;

    private int _mushroomsCount;
    private int _eggsCount;

    private void AddMushroom()
    {
        _mushroomsCount += 1;
        MushroomsAmountChanged?.Invoke(_mushroomsCount);
    }

    private void AddEgg()
    {
        _eggsCount += 1;
        EggsAmountChanged?.Invoke(_eggsCount);
    }

    private void OnEnable()
    {
        Mushroom.MushroomCollected += AddMushroom;
        Egg.EggCollected += AddEgg;
    }

    private void OnDisable()
    {
        Mushroom.MushroomCollected -= AddMushroom;
        Egg.EggCollected -= AddEgg;
    }
}
