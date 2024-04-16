using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class CharacterChanger : ObjectChanger
    {
        private const string CurrentItemKey = "CharacterKey";

        [SerializeField] private WeaponChanger _weaponChanger;
        [SerializeField] private CharacterDisplay _characterDisplay;
        [SerializeField] private Wallet _wallet;

        public event Action<string, Action<int>> LoadDataNeed;
        public event Action<string, int> SaveDataNeed;
        public event Action ItemPurchased;

        private int _currentCharacterIndex;
        private Character _character;
        private List<Character> _characters;

        private void Start()
        {
            _characters = new List<Character>();

            foreach (ScriptableObject scriptableObject in ScriptableObjects)
            {
                _characters.Add(new Character((CharacterConfig) scriptableObject));
            }
            
            for (var i = 0; i < _characters.Count; i++)
            {
                LoadDataNeed?.Invoke(CurrentItemKey + i, BuyCharacter);
            }

            LoadDataNeed?.Invoke(CurrentItemKey, data =>
            {
                _currentCharacterIndex = data;
            });

            ChangeScriptableObject(_currentCharacterIndex);
        }

        private void OnEnable()
        {
            _weaponChanger.ItemBought += UpdateDisplay;
            _wallet.GoldAmountChanged += UpdateDisplay;
            BuyButton.onClick.AddListener(TryBuyCharacter);
            InterfacePresenter.StartButtonPressed += ChooseCharacter;
        }

        private void OnDisable()
        {
            _weaponChanger.ItemBought -= UpdateDisplay;
            _wallet.GoldAmountChanged -= UpdateDisplay;
            BuyButton.onClick.RemoveListener(TryBuyCharacter);
            InterfacePresenter.StartButtonPressed -= ChooseCharacter;
        }

        public override void ChangeScriptableObject(int change)
        {
            base.ChangeScriptableObject(change);
            _currentCharacterIndex = CurrentIndex;
            SaveDataNeed?.Invoke(CurrentItemKey, _currentCharacterIndex);
            CheckOpportunityToBuy(_currentCharacterIndex);

            if (_characterDisplay != null)
            {
                _characterDisplay.DisplayCharacter(_characters[_currentCharacterIndex]);
            }
        }

        private void TryBuyCharacter()
        {
            _character = _characters[_currentCharacterIndex];

            if (_wallet.GoldAmount >= _character.Price)
            {
                BuyCharacter(_currentCharacterIndex);
                _wallet.ChangeGoldAmount(-_character.Price);
                _characterDisplay.DisplayCharacter(_characters[_currentCharacterIndex]);
            }
        }

        private void BuyCharacter(int characterIndex)
        {
            _character = _characters[characterIndex];
            _characterDisplay.ChangePriceAlertStatus(false);
            _character.BuyItem();
            ItemPurchased?.Invoke();
            SaveDataNeed?.Invoke(CurrentItemKey + characterIndex.ToString(), characterIndex);
        }

        private void ChooseCharacter()
        {
            _character = _characters[_currentCharacterIndex];

            if (_character.IsBought)
            {
                SpawnCharacter();
            }
            else
            {
                _character = _characters.OfType<Character>().LastOrDefault(character => character.IsBought);
                SpawnCharacter();
            }
        }

        private void SpawnCharacter()
        {
            Instantiate(_character.Model, Player.transform.position, Player.transform.rotation, Player.gameObject.transform);
            Player.ChangeHealth(_character.Health);
            Player.gameObject.SetActive(true);
        }
    
        private void CheckOpportunityToBuy(int index)
        {
            _character = _characters[index];
            _characterDisplay.ChangePriceAlertStatus(_character.Price <= _wallet.GoldAmount && _character.IsBought == false);

            for (int i = index + 1; i < ScriptableObjects.Length; i++)
            {
                _character = _characters[i];

                if (_character.Price <= _wallet.GoldAmount && _character.IsBought == false)
                {
                    _characterDisplay.ChangeButtonAlertStatus(true);
                    break;
                }
                else
                {
                    _characterDisplay.ChangeButtonAlertStatus(false);
                }
            }
        }

        private void UpdateDisplay()
        {
            CheckOpportunityToBuy(_currentCharacterIndex);
        }
    }
}