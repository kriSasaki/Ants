using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggResourceIcon : ResourceIcon
{
    private void OnEnable()
    {
        _playerChecker.OnResearchEggsNeeded += ResearchRecources;
    }

    private void OnDisable()
    {
        _playerChecker.OnResearchEggsNeeded -= ResearchRecources;
    }
}
