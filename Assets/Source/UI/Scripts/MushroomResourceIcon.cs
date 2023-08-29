using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomResourceIcon : ResourceIcon
{
    private void OnEnable()
    {
        Inventory.CollectedMushroom += ResearchRecources;
    }

    private void OnDisable()
    {
        Inventory.CollectedMushroom -= ResearchRecources;
    }
}
