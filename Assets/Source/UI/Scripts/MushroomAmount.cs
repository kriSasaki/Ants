using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAmount : Amount
{
    private void OnEnable()
    {
        Inventory.MushroomsAmountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        Inventory.MushroomsAmountChanged -= OnValueChanged;
    }
}
