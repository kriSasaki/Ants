using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadHandler : MonoBehaviour, ISceneLoadHandler<SceneLoadHandler>
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponChanger _weaponChanger;
    [SerializeField] private Wallet _wallet;

    private Weapon _weapon;
    private int GoldAmount => _wallet.GoldAmount;

    private void OnEnable()
    {
        _weaponChanger.OnWeaponGiven += SaveWeapon;
    }

    private void OnDisable()
    {
        _weaponChanger.OnWeaponGiven -= SaveWeapon;
    }

    public void OnSceneLoaded(SceneLoadHandler argument)
    {
        _player.GetWeapon(argument._weapon);
        _wallet.ChangeGoldAmount(argument.GoldAmount);
    }

    private void SaveWeapon(Weapon weapon)
    {
       _weapon = weapon;
    }
}
