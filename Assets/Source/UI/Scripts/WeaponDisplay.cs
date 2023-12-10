using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    private const string Buyed = "Куплено";

    [SerializeField] private TMP_Text _weaponName;
    [SerializeField] private TMP_Text _weaponDamage;
    [SerializeField] private TMP_Text _weaponPrice;
    [SerializeField] private Transform _rankStars;
    [SerializeField] private GameObject _coin;
    [SerializeField] private Transform _weaponHolder;

    public bool ItemIsBuyed { get; private set; }
    public event Action ItemChanged;

    public void DisplayWeapon(Weapon weapon)
    {
        _weaponName.text = weapon.WeaponName;
        _weaponDamage.text = "Урон: " + weapon.Damage.ToString();

        if (weapon.IsBuyed == false)
        {
            _weaponPrice.GetComponent<Button>().enabled = true;
            _coin.SetActive(true);
            _weaponPrice.text = weapon.Price.ToString();
            ItemIsBuyed = false;
            ItemChanged?.Invoke();
        }
        else
        {
            _weaponPrice.GetComponent<Button>().enabled = false;
            _coin.SetActive(false);
            _weaponPrice.text = Buyed;
            ItemIsBuyed = true;
            ItemChanged?.Invoke();
        }

        for (int star = 0; star < _rankStars.childCount; star++)
        {
            _rankStars.GetChild(star).GetChild(0).gameObject.SetActive(star < weapon.WeaponRank);
        }

        if (_weaponHolder.childCount > 0)
        {
            Destroy(_weaponHolder.GetChild(0).gameObject);
        }

        if (weapon.WeaponModel != null)
        {
            Instantiate(weapon.WeaponModel, _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
        }
    }
}
