using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Weapon;
using UnityEngine;

namespace Source.Scripts.UI
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
        public event Action ItemBought;

        private int _currentWeaponIndex;
        private Weapon.Weapon _weapon;
        private List<Weapon.Weapon> _weapons;
        private bool _isWeaponAvailable;

        private void Start()
        {
            _weapons = new List<Weapon.Weapon>();

            foreach (var scriptableObject in ScriptableObjects)
                _weapons.Add(new Weapon.Weapon((WeaponConfig)scriptableObject));

            for (var i = 0; i < _weapons.Count; i++) LoadDataNeeded?.Invoke(CurrentItemKey + i, BuyWeapon);

            LoadDataNeeded?.Invoke(CurrentItemKey, data => { _currentWeaponIndex = data; });

            ChangeScriptableObject(_currentWeaponIndex);
        }

        private void OnEnable()
        {
            _characterChanger.ItemPurchased += UpdateDisplay;
            _wallet.GoldAmountChanged += UpdateDisplay;
            Player.PlayerEnabled += TryGiveWeapon;
            BuyButton.onClick.AddListener(TryBuyWeapon);
        }

        private void OnDisable()
        {
            _characterChanger.ItemPurchased -= UpdateDisplay;
            _wallet.GoldAmountChanged -= UpdateDisplay;
            Player.PlayerEnabled -= TryGiveWeapon;
            BuyButton.onClick.RemoveListener(TryBuyWeapon);
        }

        public override void ChangeScriptableObject(int change)
        {
            base.ChangeScriptableObject(change);
            _currentWeaponIndex = CurrentIndex;
            SaveDataNeeded?.Invoke(CurrentItemKey, _currentWeaponIndex);
            CheckOpportunityToBuy(_currentWeaponIndex);

            if (_weaponDisplay != null) _weaponDisplay.DisplayWeapon(_weapons[_currentWeaponIndex]);
        }

        private void TryBuyWeapon()
        {
            _weapon = _weapons[_currentWeaponIndex];

            if (_wallet.GoldAmount >= _weapon.Price)
            {
                BuyWeapon(_currentWeaponIndex);
                _wallet.ChangeGoldAmount(-_weapon.Price);
                _weaponDisplay.DisplayWeapon(_weapons[_currentWeaponIndex]);
            }
        }

        private void BuyWeapon(int weaponIndex)
        {
            _weapon = _weapons[weaponIndex];
            _weaponDisplay.ChangePriceAlertStatus(false);
            _weapon.BuyItem();
            ItemBought?.Invoke();
            SaveDataNeeded?.Invoke(CurrentItemKey + weaponIndex, weaponIndex);
        }

        private void TryGiveWeapon()
        {
            _weapon = _weapons[_currentWeaponIndex];

            if (_weapon.IsBought)
            {
                GiveWeapon();
            }
            else
            {
                _weapon = _weapons.OfType<Weapon.Weapon>().LastOrDefault(weapon => weapon.IsBought);
                GiveWeapon();
            }
        }

        private void GiveWeapon()
        {
            Player.EquipWeapon(_weapon);

            if (_weapon.Model != null && _weapon.Price != NoModelItemIndex) Player.SpawnWeapon();
        }

        private void CheckOpportunityToBuy(int index)
        {
            _weapon = _weapons[_currentWeaponIndex];
            _isWeaponAvailable = _weapon.Price <= _wallet.GoldAmount && _weapon.IsBought;
            _weaponDisplay.ChangePriceAlertStatus(_isWeaponAvailable == false);

            for (var i = index + 1; i < ScriptableObjects.Length; i++)
            {
                _weapon = _weapons[i];

                if (_weapon.Price <= _wallet.GoldAmount && _weapon.IsBought == false)
                {
                    _weaponDisplay.ChangeButtonAlertStatus(true);
                    break;
                }

                _weaponDisplay.ChangeButtonAlertStatus(false);
            }
        }

        private void UpdateDisplay()
        {
            CheckOpportunityToBuy(_currentWeaponIndex);
        }
    }
}