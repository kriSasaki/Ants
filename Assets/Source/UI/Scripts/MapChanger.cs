using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : ScriptableObjectChanger
{
    [SerializeField] private MapDisplay _mapDisplay;

    private void OnEnable()
    {
        ChangeScriptableObject(_currentIndex);
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);

        if (_mapDisplay != null)
        {
            _mapDisplay.DisplayMap((Map)_scriptableObjects[_currentIndex]);
        }
    }
}
