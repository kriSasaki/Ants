using System;
using System.Linq;
using Source.Weapons.Scripts;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class WeaponChanger : ObjectChanger
    {
        private const string CurrentItemKey = "WeaponKey";
        private const int NoModelItemIndex = 0;

        [SerializeField] private CharacterChanger _characterChanger;
        [SerializeField] private WeaponDisplay _weaponDisplay;
        [SerializeField] private Wallet _wallet;

        public event Action<string, Action<int>> LoadDataNeeded;
        public event Action<string, int> SaveDataNeeded;
        public event Action ItemBuyed;
        private int CurrentWeapon;

        private Weapon _weapon;

        private void Start()
        {
            for (int i = 0; i < ScriptableObjects.Length; i++)
            {
                LoadDataNeeded?.Invoke(CurrentItemKey + i.ToString(), BuyWeapon);
            }

            LoadDataNeeded?.Invoke(CurrentItemKey, data => { CurrentWeapon = data; });

            ChangeScriptableObject(CurrentWeapon);
        }

        private void OnEnable()
        {
            _characterChanger.ItemBuyed += UpdateDisplay;
            _wallet.GoldAmountChanged += UpdateDisplay;
            Player.PlayerEnabled += TryGiveWeapon;
            BuyButton.onClick.AddListener(TryBuyWeapon);
        }

        private void OnDisable()
        {
            _characterChanger.ItemBuyed -= UpdateDisplay;
            _wallet.GoldAmountChanged -= UpdateDisplay;
            Player.PlayerEnabled -= TryGiveWeapon;
            BuyButton.onClick.RemoveListener(TryBuyWeapon);
        }

        public override void ChangeScriptableObject(int change)
        {
            base.ChangeScriptableObject(change);
            CurrentWeapon = CurrentIndex;
            SaveDataNeeded?.Invoke(CurrentItemKey, CurrentWeapon);
            CheckOpportunityToBuy(CurrentWeapon);

            if (_weaponDisplay != null)
            {
                _weaponDisplay.DisplayWeapon((Weapon)ScriptableObjects[CurrentIndex]);
            }
        }

        private void TryBuyWeapon()
        {
            _weapon = (Weapon)ScriptableObjects[CurrentWeapon];

            if (_wallet.GoldAmount >= _weapon.Price)
            {
                BuyWeapon(CurrentWeapon);
                _wallet.ChangeGoldAmount(-_weapon.Price);
                _weaponDisplay.DisplayWeapon((Weapon)ScriptableObjects[CurrentWeapon]);
            }
        }

        private void BuyWeapon(int weaponIndex)
        {
            _weapon = (Weapon)ScriptableObjects[weaponIndex];
            _weaponDisplay.ChangePriceAlertStatus(false);
            _weapon.BuyItem();
            ItemBuyed?.Invoke();
            SaveDataNeeded?.Invoke(CurrentItemKey + weaponIndex.ToString(), weaponIndex);
        }

        private void TryGiveWeapon()
        {
            _weapon = (Weapon)ScriptableObjects[CurrentWeapon];
        
            if (_weapon.IsBuyed)
            {
                GiveWeapon();
            }
            else
            {
                _weapon = ScriptableObjects.OfType<Weapon>().LastOrDefault(weapon => weapon.IsBuyed);
                GiveWeapon();
            }
        }

        private void GiveWeapon()
        {
            Player.EquipWeapon(_weapon);

            if (_weapon.Model != null && _weapon.Price != NoModelItemIndex)
            {
                Player.SpawnWeapon();
            }
        }

        private void CheckOpportunityToBuy(int index)
        {
            _weapon = (Weapon)ScriptableObjects[index];
            _weaponDisplay.ChangePriceAlertStatus(_weapon.Price <= _wallet.GoldAmount && _weapon.IsBuyed == false);

            for (int i = index + 1; i < ScriptableObjects.Length; i++)
            {
                _weapon = (Weapon)ScriptableObjects[i];

                if (_weapon.Price <= _wallet.GoldAmount && _weapon.IsBuyed == false)
                {
                    _weaponDisplay.ChangeButtonAlertStatus(true);
                    break;
                }

                _weaponDisplay.ChangeButtonAlertStatus(false);
            }
        }
    
        private void UpdateDisplay()
        {
            CheckOpportunityToBuy(CurrentWeapon);
        }
    }
}