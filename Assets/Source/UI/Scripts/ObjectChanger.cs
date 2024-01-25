using UnityEngine;

public class ObjectChanger : MonoBehaviour
{
    [SerializeField] protected ScriptableObject[] _scriptableObjects;

    protected IStorageService _storageService;
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

    public void SetIndex(int index)
    {
        Debug.Log(index);
        _currentIndex = index;
    }
}
