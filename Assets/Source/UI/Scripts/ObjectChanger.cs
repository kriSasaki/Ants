using UnityEngine;
using UnityEngine.UI;

public class ObjectChanger : MonoBehaviour
{
    [SerializeField] protected ScriptableObject[] _scriptableObjects;
    [SerializeField] protected Button _buyButton;
    [SerializeField] protected GameObject _leftButton;
    [SerializeField] protected GameObject _rightButton;

    protected InterfaceVisualizer _interfaceVisualizer;
    protected Player _player;
    protected int _currentIndex = 0;

    private readonly int _firstElement = 0;
    
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
        
        _leftButton.SetActive(_currentIndex != _firstElement);
        _rightButton.SetActive(_currentIndex != _scriptableObjects.Length-1);
    }
}
