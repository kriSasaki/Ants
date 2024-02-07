using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    private const string Buyed = "Куплено";

    [SerializeField] private TMP_Text _weaponDamage;
    [SerializeField] private TMP_Text _weaponPrice;
    [SerializeField] private Transform _rankStars;
    [SerializeField] private GameObject _coin;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] protected GameObject _alert;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    public bool ItemIsBuyed { get; private set; }
    public event Action ItemChanged;

    public void DisplayWeapon(Weapon weapon)
    {
        _weaponDamage.text = weapon.Damage.ToString();
        _particleSystem.Stop();
        var main = _particleSystem.main;
        main.startColor = weapon.Color;
        _particleSystem.Play();

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
            _rankStars.GetChild(star).GetChild(0).gameObject.SetActive(star < weapon.Rank);
        }

        if (_weaponHolder.childCount > 0)
        {
            Destroy(_weaponHolder.GetChild(0).gameObject);
        }

        if (weapon.Model != null)
        {
            Instantiate(weapon.Model, _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
        }
    }

    public void SetAlertStatus(bool isNewItemAvailable)
    {
        if (isNewItemAvailable)
        {
            _rightButton.GetComponent<ScaleChanger>().StartTween();
            _alert.SetActive(true);
        }
        else
        {
            _rightButton.GetComponent<ScaleChanger>().StopTween();
            _alert.SetActive(false);
        }
    }

    public void ChangeInteractivity(bool isEnable)
    {
        _leftButton.enabled = isEnable;
        _rightButton.enabled = isEnable;
        _weaponPrice.gameObject.GetComponent<Button>().enabled = isEnable;
    }
}
