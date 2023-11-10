using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsAmount : Amount
{
    private void OnEnable()
    {
        Inventory.EggsAmountChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        Inventory.EggsAmountChanged -= OnValueChanged;
    }
}
