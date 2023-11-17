using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceChecker : MonoBehaviour
{
    [SerializeField] private int _mushrooms;
    [SerializeField] private int _eggs;
    [SerializeField] private int _legs;

    public int Mushrooms => _mushrooms;
    public int Legs => _legs;
    public int Eggs => _eggs;
    public bool MushroomCollected { get; private set; }
    public bool LegsCollected { get; private set; }
    public bool EggsCollected { get; private set; }

    private int _mushroomCount;
    private int _legsCount;
    private int _eggsCount;

    private void OnEnable()
    {
        Inventory.MushroomsAmountChanged += CheckMushrooms;
        Inventory.EggsAmountChanged+= CheckEggs;
        Inventory.LegsAmountChanged += CheckLegs;
    }

    private void OnDisable()
    {
        Inventory.MushroomsAmountChanged -= CheckMushrooms;
        Inventory.EggsAmountChanged -= CheckEggs;
        Inventory.LegsAmountChanged -= CheckLegs;
    }

    private void CheckMushrooms(int amount)
    {
        _mushroomCount = amount;
        ResearchRecources();
    }

    private void CheckEggs(int amount)
    {
        _eggsCount= amount;
        ResearchRecources();
    }

    private void CheckLegs(int amount)
    {
        _legsCount = amount;
        ResearchRecources();
    }

    private void ResearchRecources()
    {
        if (_mushroomCount >= _mushrooms)
        {
            MushroomCollected = true;
        }

        if (_legsCount >= _legs)
        {
            LegsCollected = true;
        }

        if(_eggsCount >= _eggs)
        {
            EggsCollected = true;
        }
    }
}
