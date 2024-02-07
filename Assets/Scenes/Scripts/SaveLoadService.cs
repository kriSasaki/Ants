using System;
using UnityEngine;

public class SaveLoadService : MonoBehaviour
{
    [SerializeField] private WeaponChanger _weaponChanger;
    [SerializeField] private CharacterChanger _characterChanger;
    [SerializeField] private LevelService _levelService;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LeaderBoardDisplay _leaderBoardDisplay;

    private IStorageService _storageService;
    private int _data;
    //private List<ISaveLoadItem> _items = new List<ISaveLoadItem>();

    private void Awake()
    {
        _storageService = new ObjectSaver();
    }

    private void OnEnable()
    {
        _levelService.OnLoadDataNeeded += Load;
        _levelService.OnSaveDataNeeded += Save;
        _weaponChanger.OnLoadDataNeeded += Load;
        _weaponChanger.OnSaveDataNeeded += Save;
        _characterChanger.OnLoadDataNeeded += Load;
        _characterChanger.OnSaveDataNeeded += Save;
        _wallet.OnLoadDataNeeded += Load;
        _wallet.OnSaveDataNeeded += Save;
        _leaderBoardDisplay.OnLoadDataNeeded += Load;
        _leaderBoardDisplay.OnSaveDataNeeded += Save;
    }

    private void OnDisable()
    {
        _levelService.OnLoadDataNeeded -= Load;
        _levelService.OnSaveDataNeeded -= Save;
        _weaponChanger.OnLoadDataNeeded -= Load;
        _weaponChanger.OnSaveDataNeeded -= Save;
        _characterChanger.OnLoadDataNeeded -= Load;
        _characterChanger.OnSaveDataNeeded -= Save;
        _wallet.OnLoadDataNeeded -= Load;
        _wallet.OnSaveDataNeeded -= Save;
        _leaderBoardDisplay.OnLoadDataNeeded -= Load;
        _leaderBoardDisplay.OnSaveDataNeeded -= Save;
    }

    //public void Register(ISaveLoadItem saveLoadItem)
    //{
    //    _items.Add(saveLoadItem);
    //    saveLoadItem.OnLoadDataNeeded += Load;
    //    saveLoadItem.OnSaveDataNeeded += Save;
    //}

    //public void Unregister(ISaveLoadItem saveLoadItem)
    //{
    //    _items.Remove(saveLoadItem);
    //    saveLoadItem.OnLoadDataNeeded -= Load;
    //    saveLoadItem.OnSaveDataNeeded -= Save;
    //}

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
