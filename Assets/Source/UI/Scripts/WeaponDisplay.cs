using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _weaponName;
    [SerializeField] private TMP_Text _weaponDamage;
    [SerializeField] private TMP_Text _weaponPrice;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private Transform _rankStars;
    [SerializeField] private Transform _weaponHolder;

    public void DisplayWeapon(Weapon weapon)
    {
        _weaponName.text = weapon.WeaponName;
        _weaponDamage.text = "Урон: " + weapon.Damage.ToString();

        if (weapon.IsBuyed == false)
        {
            _weaponPrice.gameObject.SetActive(true);
            _weaponPrice.text = weapon.Price.ToString();
            _equipButton.SetActive(false);
        }
        else if (weapon.IsEquiped == false)
        {
            _equipButton.SetActive(true);
            _weaponPrice.gameObject.SetActive(false);
        }
        else
        {
            _equipButton.SetActive(false);
            _weaponPrice.gameObject.SetActive(false);
        }

        for(int star = 0; star < _rankStars.childCount; star++)
        {
            _rankStars.GetChild(star).GetChild(0).gameObject.SetActive(star < weapon.WeaponRank);
        }

        if (_weaponHolder.childCount > 0)
        {
            Destroy(_weaponHolder.GetChild(0).gameObject);
        }

        Instantiate(weapon.WeaponModel, _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
    }
}
