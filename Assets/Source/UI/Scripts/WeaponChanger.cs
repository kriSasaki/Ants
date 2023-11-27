using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class WeaponChanger : ScriptableObjectChanger
{
    [SerializeField] private WeaponDisplay _weaponDisplay;
    [SerializeField] private Player _player;
    [SerializeField] private Wallet _wallet;

    private Weapon _weapon;

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

    public void EquipWeapon()
    {
        _player.GetWeapon((Weapon)_scriptableObjects[_currentIndex]);
        _weaponDisplay.DisplayWeapon((Weapon)_scriptableObjects[_currentIndex]);
    }
}
