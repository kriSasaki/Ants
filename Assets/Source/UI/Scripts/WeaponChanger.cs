using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using IJunior.TypedScenes;
using System;

public class WeaponChanger : ScriptableObjectChanger
{
    [SerializeField] private WeaponDisplay _weaponDisplay;
    [SerializeField] private Player _player;
    [SerializeField] private Wallet _wallet;
    
    public int CurrentWeapon { get; private set; }
    public event Action<Weapon> OnWeaponGiven;

    private InterfaceManager _interfaceManager;
    private Weapon _weapon;

    private void Awake()
    {
        _interfaceManager = GetComponentInParent<InterfaceManager>();
    }

    private void OnEnable()
    {
        ChangeScriptableObject(_currentIndex);
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);

        if (_weaponDisplay != null)
        {
            _weaponDisplay.DisplayWeapon((Weapon)_scriptableObjects[_currentIndex]);
        }
    }

    public void BuyWeapon()
    {
        _weapon = (Weapon)_scriptableObjects[_currentIndex];

        if (_wallet.GoldAmount >= _weapon.Price)
        {
            _weapon.IsBuyed = true;
            _weaponDisplay.DisplayWeapon((Weapon)_scriptableObjects[_currentIndex]);
            _wallet.ChangeGoldAmount(-_weapon.Price);
        }
    }

    public void StoreWeapon()
    {
        CurrentWeapon = _currentIndex;
        GiveWeapon();
        _weaponDisplay.DisplayWeapon((Weapon)_scriptableObjects[_currentIndex]);
    }

    private void GiveWeapon()
    {
        OnWeaponGiven?.Invoke((Weapon)_scriptableObjects[CurrentWeapon]);
        _player.GetWeapon((Weapon)_scriptableObjects[CurrentWeapon]);
    }
}
