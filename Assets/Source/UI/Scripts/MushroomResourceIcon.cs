using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomResourceIcon : ResourceIcon
{
    private void OnEnable()
    {
        Inventory.MushroomsAmountChanged += ResearchRecources;
        PlayerChecker.PlayerEnter += SetAmount;
    }

    private void OnDisable()
    {
        Inventory.MushroomsAmountChanged -= ResearchRecources;
        PlayerChecker.PlayerEnter -= SetAmount;
    }
}
