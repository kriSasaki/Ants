using System;
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

        private int _currentCharacter;
        private Character _character;

        private void Start()
        {
            for (int i = 0; i < ScriptableObjects.Length; i++)
            {
                LoadDataNeed?.Invoke(CurrentItemKey + i.ToString(), BuyCharacter);
            }

            LoadDataNeed?.Invoke(CurrentItemKey, data =>
            {
                _currentCharacter = data;
            });

            ChangeScriptableObject(_currentCharacter);
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
            _currentCharacter = CurrentIndex;
            SaveDataNeed?.Invoke(CurrentItemKey, _currentCharacter);
            CheckOpportunityToBuy(_currentCharacter);

            if (_characterDisplay != null)
            {
                _characterDisplay.DisplayCharacter((Character)ScriptableObjects[CurrentIndex]);
            }
        }

        private void TryBuyCharacter()
        {
            _character = (Character)ScriptableObjects[_currentCharacter];

            if (_wallet.GoldAmount >= _character.Price)
            {
                BuyCharacter(_currentCharacter);
                _wallet.ChangeGoldAmount(-_character.Price);
                _characterDisplay.DisplayCharacter((Character)ScriptableObjects[_currentCharacter]);
            }
        }

        private void BuyCharacter(int characterIndex)
        {
            _character = (Character)ScriptableObjects[characterIndex];
            _characterDisplay.ChangePriceAlertStatus(false);
            _character.IsBought = true;
            ItemPurchased?.Invoke();
            SaveDataNeed?.Invoke(CurrentItemKey + characterIndex.ToString(), characterIndex);
        }

        private void ChooseCharacter()
        {
            _character = (Character)ScriptableObjects[_currentCharacter];

            if (_character.IsBought)
            {
                SpawnCharacter();
            }
            else
            {
                _character = ScriptableObjects.OfType<Character>().LastOrDefault(character => character.IsBought);
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
            _character = (Character)ScriptableObjects[index];
            _characterDisplay.ChangePriceAlertStatus(_character.Price <= _wallet.GoldAmount && _character.IsBought == false);

            for (int i = index + 1; i < ScriptableObjects.Length; i++)
            {
                _character = (Character)ScriptableObjects[i];

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
            CheckOpportunityToBuy(_currentCharacter);
        }
    }
}