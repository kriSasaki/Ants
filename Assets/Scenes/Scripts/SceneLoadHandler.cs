using System;
using UnityEngine;

public class SceneLoadHandler : MonoBehaviour
{
    [SerializeField] private WeaponChanger _weaponChanger;
    [SerializeField] private CharacterChanger _characterChanger;
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LeaderBoardDisplay _leaderBoardDisplay;

    private IStorageService _storageService;
    private int _data;

    private void Awake()
    {
        _storageService = new ObjectSaver();
    }

    private void OnEnable()
    {
        _levelManager.OnLoadDataNeeded += Load;
        _levelManager.OnSaveDataNeeded += Save;
        _weaponChanger.OnLoadDataNeeded += Load;
        _weaponChanger.OnSaveDataNeeded += Save;
        _characterChanger.OnLoadDataNeeded += Load;
        _characterChanger.OnSaveDataNeeded += Save;
        _wallet.OnLoadDataNeeded += Load;
        _wallet.OnSaveDataNeeded += Save;
    }

    private void OnDisable()
    {
        _levelManager.OnLoadDataNeeded -= Load;
        _levelManager.OnSaveDataNeeded -= Save;
        _weaponChanger.OnLoadDataNeeded -= Load;
        _weaponChanger.OnSaveDataNeeded -= Save;
        _characterChanger.OnLoadDataNeeded -= Load;
        _characterChanger.OnSaveDataNeeded -= Save;
        _wallet.OnLoadDataNeeded -= Load;
        _wallet.OnSaveDataNeeded -= Save;
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Load(string key, Action<int> callback)
    {
        _storageService.Load(key, data =>
        {
            _data = data;
        });

        callback?.Invoke(_data);
    }

    private void Save(string key, int data)
    {
        _storageService.Save(key, data);
    }
}
