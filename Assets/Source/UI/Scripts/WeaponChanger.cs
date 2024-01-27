using System;
using UnityEngine;

public class WeaponChanger : ObjectChanger
{
    private const string CurrentItemKey = "WeaponKey";

    [SerializeField] private WeaponDisplay _weaponDisplay;
    [SerializeField] private Wallet _wallet;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public int CurrentWeapon { get; private set; }

    private Weapon _weapon;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerTransmitter>().Player;
        _interfaceManager = GetComponentInParent<InterfaceManager>();
    }

    private void Start()
    {
        OnLoadDataNeeded?.Invoke(CurrentItemKey, data =>
        {
            CurrentWeapon = data;
        });

        ChangeScriptableObject(CurrentWeapon);
        BuyWeapon();
    }

    private void OnEnable()
    {
        _player.OnPlayerEnable += GiveWeapon;
    }

    private void OnDisable()
    {
        _player.OnPlayerEnable -= GiveWeapon;
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);
        CurrentWeapon = _currentIndex;
        OnSaveDataNeeded?.Invoke(CurrentItemKey, CurrentWeapon);

        if (_weaponDisplay != null)
        {
            _weaponDisplay.DisplayWeapon((Weapon)_scriptableObjects[_currentIndex]);
        }

    }

    public void BuyWeapon()
    {
        _weapon = (Weapon)_scriptableObjects[CurrentWeapon];

        if (_wallet.GoldAmount >= _weapon.Price)
        {
            _weapon.BuyItem();
            _weaponDisplay.DisplayWeapon(_weapon);
            _wallet.ChangeGoldAmount(-_weapon.Price);
        }
    }

    private void GiveWeapon()
    {
        _weapon = (Weapon)_scriptableObjects[CurrentWeapon];
        _player.GetWeapon(_weapon);

        if(_weapon.Model != null)
        {
            _player.SpawnWeapon();
        }
    }
}
