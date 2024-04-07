using System;
using Source.Weapons.Scripts;
using TMPro;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class WeaponDisplay : ItemDisplay
    {
        private const string BuyedString = "Куплено";

        [SerializeField] private TMP_Text _weaponDamage;
        [SerializeField] private GameObject _coin;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private ParticleSystem _particleSystem;

        public bool ItemIsBuyed { get; private set; }
        public event Action ItemChanged;

        private GameObject _weapon;

        public void DisplayWeapon(Weapon weapon)
        {
            _weaponDamage.text = weapon.Damage.ToString();
            _particleSystem.Stop();
            var main = _particleSystem.main;
            main.startColor = weapon.Color;
            _particleSystem.Play();
            Buyed.enabled = weapon.IsBuyed;
            Price.enabled = !weapon.IsBuyed;

            if (weapon.IsBuyed == false)
            {
                BuyButton.enabled = true;
                _coin.SetActive(true);
                Price.text = weapon.Price.ToString();
                ItemIsBuyed = false;
                ItemChanged?.Invoke();
            }
            else
            {
                BuyButton.enabled = false;
                _coin.SetActive(false);
                Price.text = Lean.Localization.LeanLocalization.GetTranslationText(BuyedString);
                ItemIsBuyed = true;
                ItemChanged?.Invoke();
            }

            RankStars.ShowStars(weapon.Rank);
            
            if (_weaponHolder.childCount > 0)
            {
                Destroy(_weapon);
            }

            if (weapon.Model != null)
            {
                _weapon = Instantiate(weapon.Model, _weaponHolder.position, _weaponHolder.rotation, _weaponHolder);
            }
        }
    }
}