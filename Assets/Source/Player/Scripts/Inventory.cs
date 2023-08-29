using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    public static event Action<int> MushroomsAmountChanged;
    public static event Action CollectedMushroom;

    private int _mushroomsCount;

    private void AddMushroom()
    {
        _mushroomsCount += 1;
        MushroomsAmountChanged?.Invoke(_mushroomsCount);
    }

    private void OnEnable()
    {
        Mushroom.MushroomCollected += AddMushroom;
    }

    private void OnDisable()
    {
        Mushroom.MushroomCollected -= AddMushroom;
    }
}
