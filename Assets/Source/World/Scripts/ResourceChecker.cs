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
    public bool MushroomCollected = _mushroomCollected;

    private int _mushroomCount;
    private static bool _mushroomCollected = false;

    private void OnEnable()
    {
        Inventory.CollectedMushroom += ResearchRecources;
    }

    private void OnDisable()
    {
        Inventory.CollectedMushroom -= ResearchRecources;
    }

    private void ResearchRecources()
    {
        _mushroomCount += 1;

        if (_mushroomCount >= _mushrooms)
        {
            _mushroomCollected = true;
        }
    }
}
