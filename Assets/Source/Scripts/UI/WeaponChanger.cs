using System;
using System.Linq;
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
        private int CurrentWeapon;

        private Weapon.Weapon _weapon;

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
            CurrentWeapon = CurrentIndex;
            SaveDataNeeded?.Invoke(CurrentItemKey, CurrentWeapon);
            CheckOpportunityToBuy(CurrentWeapon);

            if (_weaponDisplay != null)
            {
                _weaponDisplay.DisplayWeapon((Weapon.Weapon)ScriptableObjects[CurrentIndex]);
            }
        }

        private void TryBuyWeapon()
        {
            _weapon = (Weapon.Weapon)ScriptableObjects[CurrentWeapon];

            if (_wallet.GoldAmount >= _weapon.Price)
            {
                BuyWeapon(CurrentWeapon);
                _wallet.ChangeGoldAmount(-_weapon.Price);
                _weaponDisplay.DisplayWeapon((Weapon.Weapon)ScriptableObjects[CurrentWeapon]);
            }
        }

        private void BuyWeapon(int weaponIndex)
        {
            _weapon = (Weapon.Weapon)ScriptableObjects[weaponIndex];
            _weaponDisplay.ChangePriceAlertStatus(false);
            _weapon.BuyItem();
            ItemBought?.Invoke();
            SaveDataNeeded?.Invoke(CurrentItemKey + weaponIndex.ToString(), weaponIndex);
        }

        private void TryGiveWeapon()
        {
            _weapon = (Weapon.Weapon)ScriptableObjects[CurrentWeapon];
        
            if (_weapon.IsBought)
            {
                GiveWeapon();
            }
            else
            {
                _weapon = ScriptableObjects.OfType<Weapon.Weapon>().LastOrDefault(weapon => weapon.IsBought);
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
            _weapon = (Weapon.Weapon)ScriptableObjects[index];
            _weaponDisplay.ChangePriceAlertStatus(_weapon.Price <= _wallet.GoldAmount && _weapon.IsBought == false);

            for (int i = index + 1; i < ScriptableObjects.Length; i++)
            {
                _weapon = (Weapon.Weapon)ScriptableObjects[i];

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
            CheckOpportunityToBuy(CurrentWeapon);
        }
    }
}