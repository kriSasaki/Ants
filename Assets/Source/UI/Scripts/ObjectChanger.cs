using UnityEngine;
using UnityEngine.UI;

public class ObjectChanger : MonoBehaviour
{
    [SerializeField] protected ScriptableObject[] _scriptableObjects;
    [SerializeField] protected Button _buyButton;

    protected InterfaceManager _interfaceManager;
    protected Player _player;
    protected int _currentIndex = 0;

    public virtual void ChangeScriptableObject(int change)
    {
        _currentIndex += change;

        if (_currentIndex < 0)
        {
            _currentIndex = _scriptableObjects.Length - 1;
        }
        else if (_currentIndex > _scriptableObjects.Length - 1)
        {
            _currentIndex = 0;
        }
    }
}
