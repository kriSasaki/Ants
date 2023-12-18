using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomResourceIcon : ResourceIcon
{
    private void OnEnable()
    {
        _playerChecker.OnResearchMushroomNeeded += ResearchRecources;
    }

    private void OnDisable()
    {
        _playerChecker.OnResearchMushroomNeeded -= ResearchRecources;
    }
}
