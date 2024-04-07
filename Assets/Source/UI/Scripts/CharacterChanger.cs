using System;
using System.Linq;
using Source.Player.Scripts;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class CharacterChanger : ObjectChanger
    {
        private const string CurrentItemKey = "CharacterKey";

        [SerializeField] private WeaponChanger _weaponChanger;
        [SerializeField] private CharacterDisplay _characterDisplay;
        [SerializeField] private Wallet _wallet;

        public event Action<string, Action<int>> LoadDataNeed;
        public event Action<string, int> SaveDataNeed;
        public event Action ItemBuyed;
        private int CurrentCharacter;

        private Character _character;

        private void Start()
        {
            for (int i = 0; i < ScriptableObjects.Length; i++)
            {
                LoadDataNeed?.Invoke(CurrentItemKey + i.ToString(), BuyCharacter);
            }

            LoadDataNeed?.Invoke(CurrentItemKey, data =>
            {
                CurrentCharacter = data;
            });

            ChangeScriptableObject(CurrentCharacter);
        }

        private void OnEnable()
        {
            _weaponChanger.ItemBuyed += UpdateDisplay;
            _wallet.GoldAmountChanged += UpdateDisplay;
            BuyButton.onClick.AddListener(TryBuyCharacter);
            InterfacePresenter.StartButtonPressed += ChooseCharacter;
        }

        private void OnDisable()
        {
            _weaponChanger.ItemBuyed -= UpdateDisplay;
            _wallet.GoldAmountChanged -= UpdateDisplay;
            BuyButton.onClick.RemoveListener(TryBuyCharacter);
            InterfacePresenter.StartButtonPressed -= ChooseCharacter;
        }

        public override void ChangeScriptableObject(int change)
        {
            base.ChangeScriptableObject(change);
            CurrentCharacter = CurrentIndex;
            SaveDataNeed?.Invoke(CurrentItemKey, CurrentCharacter);
            CheckOpportunityToBuy(CurrentCharacter);

            if (_characterDisplay != null)
            {
                _characterDisplay.DisplayCharacter((Character)ScriptableObjects[CurrentIndex]);
            }
        }

        private void TryBuyCharacter()
        {
            _character = (Character)ScriptableObjects[CurrentCharacter];

            if (_wallet.GoldAmount >= _character.Price)
            {
                BuyCharacter(CurrentCharacter);
                _wallet.ChangeGoldAmount(-_character.Price);
                _characterDisplay.DisplayCharacter((Character)ScriptableObjects[CurrentCharacter]);
            }
        }

        private void BuyCharacter(int characterIndex)
        {
            _character = (Character)ScriptableObjects[characterIndex];
            _characterDisplay.ChangePriceAlertStatus(false);
            _character.IsBuyed = true;
            ItemBuyed?.Invoke();
            SaveDataNeed?.Invoke(CurrentItemKey + characterIndex.ToString(), characterIndex);
        }

        private void ChooseCharacter()
        {
            _character = (Character)ScriptableObjects[CurrentCharacter];

            if (_character.IsBuyed)
            {
                SpawnCharacter();
            }
            else
            {
                _character = ScriptableObjects.OfType<Character>().LastOrDefault(character => character.IsBuyed);
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
            _characterDisplay.ChangePriceAlertStatus(_character.Price <= _wallet.GoldAmount && _character.IsBuyed == false);

            for (int i = index + 1; i < ScriptableObjects.Length; i++)
            {
                _character = (Character)ScriptableObjects[i];

                if (_character.Price <= _wallet.GoldAmount && _character.IsBuyed == false)
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
            CheckOpportunityToBuy(CurrentCharacter);
        }
    }
}