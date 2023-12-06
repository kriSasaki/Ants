using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectChanger : MonoBehaviour
{
    [SerializeField] protected ScriptableObject[] _scriptableObjects;

    protected int _currentIndex = 0;

    public virtual void ChangeScriptableObject(int change)
    {
        _currentIndex += change;

        if (_currentIndex < 0)
        {
            _currentIndex = _scriptableObjects.Length - 1;
        }
        else if(_currentIndex > _scriptableObjects.Length - 1)
        {
            _currentIndex = 0;
        }
    }
}
