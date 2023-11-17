using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggResourceIcon : ResourceIcon
{
    private void OnEnable()
    {
        Inventory.EggsAmountChanged += ResearchRecources;
        PlayerChecker.PlayerEnter += SetAmount;
    }

    private void OnDisable()
    {
        Inventory.EggsAmountChanged -= ResearchRecources;
        PlayerChecker.PlayerEnter -= SetAmount;
    }
}
