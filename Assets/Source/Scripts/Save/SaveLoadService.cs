using System;
using Source.Scripts.UI;
using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.Save
{
    public class SaveLoadService : MonoBehaviour
    {
        [SerializeField] private WeaponChanger _weaponChanger;
        [SerializeField] private CharacterChanger _characterChanger;
        [SerializeField] private LevelService _levelService;
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
            _levelService.LoadDataNeeded += Load;
            _levelService.SaveDataNeeded += Save;
            _weaponChanger.LoadDataNeeded += Load;
            _weaponChanger.SaveDataNeeded += Save;
            _characterChanger.LoadDataNeed += Load;
            _characterChanger.SaveDataNeed += Save;
            _wallet.LoadDataNeeded += Load;
            _wallet.SaveDataNeeded += Save;
            _leaderBoardDisplay.LoadDataNeeded += Load;
            _leaderBoardDisplay.SaveDataNeeded += Save;
        }

        private void OnDisable()
        {
            _levelService.LoadDataNeeded -= Load;
            _levelService.SaveDataNeeded -= Save;
            _weaponChanger.LoadDataNeeded -= Load;
            _weaponChanger.SaveDataNeeded -= Save;
            _characterChanger.LoadDataNeed -= Load;
            _characterChanger.SaveDataNeed -= Save;
            _wallet.LoadDataNeeded -= Load;
            _wallet.SaveDataNeeded -= Save;
            _leaderBoardDisplay.LoadDataNeeded -= Load;
            _leaderBoardDisplay.SaveDataNeeded -= Save;
        }

        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        private void Load(string key, Action<int> callback)
        {
            _storageService.Load(key, data => { _data = data; });

            callback?.Invoke(_data);
        }

        private void Save(string key, int data)
        {
            _storageService.Save(key, data);
        }
    }
}