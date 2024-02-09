using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WeaponChanger : ObjectChanger, ISaveLoadItem
{
    private const string CurrentItemKey = "WeaponKey";
    private const int NoModelItemIndex = 0;

    [SerializeField] private WeaponDisplay _weaponDisplay;
    [SerializeField] private Wallet _wallet;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public int CurrentWeapon { get; private set; }

    private Weapon _weapon;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerTransmitter>().Player;
        _interfaceVisualizer = GetComponentInParent<InterfaceVisualizer>();
    }

    private void Start()
    {
        for (int i = 0; i < _scriptableObjects.Length; i++)
        {
            OnLoadDataNeeded?.Invoke(CurrentItemKey + i.ToString(), BuyWeapon);
        }

        OnLoadDataNeeded?.Invoke(CurrentItemKey, data => { CurrentWeapon = data; });

        ChangeScriptableObject(CurrentWeapon);
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(delegate { TryBuyWeapon(CurrentWeapon); });
        _player.OnPlayerEnable += GiveWeapon;
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(delegate { TryBuyWeapon(CurrentWeapon); });
        _player.OnPlayerEnable -= GiveWeapon;
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);
        CurrentWeapon = _currentIndex;
        OnSaveDataNeeded?.Invoke(CurrentItemKey, CurrentWeapon);
        CheckOpportunityToBuy(CurrentWeapon);

        if (_weaponDisplay != null)
        {
            _weaponDisplay.DisplayWeapon((Weapon)_scriptableObjects[_currentIndex]);
        }
    }

    private void TryBuyWeapon(int weaponIndex)
    {
        _weapon = (Weapon)_scriptableObjects[weaponIndex];

        if (_wallet.GoldAmount >= _weapon.Price)
        {
            BuyWeapon(weaponIndex);
            _wallet.ChangeGoldAmount(-_weapon.Price);
            _weaponDisplay.DisplayWeapon(_weapon);
        }
    }

    private void BuyWeapon(int weaponIndex)
    {
        _weapon = (Weapon)_scriptableObjects[weaponIndex];
        _weaponDisplay.ChangePriceAlertStatus(false);
        _weapon.BuyItem();
        OnSaveDataNeeded?.Invoke(CurrentItemKey + weaponIndex.ToString(), weaponIndex);
    }

    private void GiveWeapon()
    {
        _weapon = (Weapon)_scriptableObjects[CurrentWeapon];
        _player.GetWeapon(_weapon);

        if (_weapon.Model != null && CurrentWeapon != NoModelItemIndex)
        {
            _player.SpawnWeapon();
        }
    }

    private void CheckOpportunityToBuy(int index)
    {
        _weapon = (Weapon)_scriptableObjects[index];
        _weaponDisplay.ChangePriceAlertStatus(_weapon.Price <= _wallet.GoldAmount && _weapon.IsBuyed == false);

        for (int i = index + 1; i < _scriptableObjects.Length; i++)
        {
            _weapon = (Weapon)_scriptableObjects[i];

            if (_weapon.Price <= _wallet.GoldAmount && _weapon.IsBuyed == false)
            {
                _weaponDisplay.ChangeButtonAlertStatus(true);
                break;
            }
            else
            {
                _weaponDisplay.ChangeButtonAlertStatus(false);
            }
        }
    }
}