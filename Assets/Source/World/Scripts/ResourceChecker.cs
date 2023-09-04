using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChecker : MonoBehaviour
{
    [SerializeField] private int _mushrooms;
    [SerializeField] private int _legs;

    public int Mushrooms => _mushrooms;
    public int Legs => _legs;
    public bool MushroomCollected { get; private set; }

    private int _mushroomCount;

    private void OnEnable()
    {
        Inventory.MushroomsAmountChanged += ResearchRecources;
    }

    private void OnDisable()
    {
        Inventory.MushroomsAmountChanged -= ResearchRecources;
    }

    private void ResearchRecources(int amount)
    {
        _mushroomCount = amount;

        if (_mushroomCount >= _mushrooms)
        {
            MushroomCollected = true;
        }
    }
}
