using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using IJunior.TypedScenes;
using System;
using System.Reflection;

public class WeaponChanger : ScriptableObjectChanger
{
    [SerializeField] private WeaponDisplay _weaponDisplay;
    [SerializeField] private Player _player;
    [SerializeField] private Wallet _wallet;
    
    public int CurrentWeapon { get; private set; }

    private InterfaceManager _interfaceManager;
    private Weapon _weapon;

    private void Awake()
    {
        _interfaceManager = GetComponentInParent<InterfaceManager>();
        ChangeScriptableObject(0);
    }

    private void OnEnable()
    {
        _interfaceManager.OnGameStarted += GiveWeapon;
    }

    private void OnDisable()
    {
        _interfaceManager.OnGameStarted -= GiveWeapon;
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);
        CurrentWeapon = _currentIndex;

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
            _weaponDisplay.DisplayWeapon(_weapon);
            _wallet.ChangeGoldAmount(-_weapon.Price);
        }
    }

    private void GiveWeapon()
    {
        _weapon = (Weapon)_scriptableObjects[CurrentWeapon];
        _player.GetWeapon(_weapon);

        if(_weapon.WeaponModel != null)
        {
            _player.SpawnWeapon();
        }
    }
}
