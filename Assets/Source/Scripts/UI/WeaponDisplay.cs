using System;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class WeaponDisplay : ItemDisplay
    {
        private const string BoughtString = "Куплено";

        [SerializeField] private TMP_Text _weaponDamage;
        [SerializeField] private GameObject _coin;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private ParticleSystem _particleSystem;

        private GameObject _weapon;

        public event Action ItemChanged;

        public bool ItemIsBought { get; private set; }

        public void DisplayWeapon(Weapon.Weapon weaponConfig)
        {
            _weaponDamage.text = weaponConfig.Damage.ToString();
            _particleSystem.Stop();
            var main = _particleSystem.main;
            main.startColor = weaponConfig.Color;
            _particleSystem.Play();
            Bought.enabled = weaponConfig.IsBought;
            Price.enabled = !weaponConfig.IsBought;

            if (weaponConfig.IsBought == false)
            {
                BuyButton.enabled = true;
                _coin.SetActive(true);
                Price.text = weaponConfig.Price.ToString();
                ItemIsBought = false;
                ItemChanged?.Invoke();
            }
            else
            {
                BuyButton.enabled = false;
                _coin.SetActive(false);
                Price.text = Lean.Localization.LeanLocalization.GetTranslationText(BoughtString);
                ItemIsBought = true;
                ItemChanged?.Invoke();
            }

            RankStars.ShowStars(weaponConfig.Rank);

            if (_weaponHolder.childCount > 0)
            {
                Destroy(_weapon);
            }

            if (weaponConfig.Model != null)
            {
                _weapon = Instantiate(
                    weaponConfig.Model,
                    _weaponHolder.position,
                    _weaponHolder.rotation,
                    _weaponHolder);
            }
        }
    }
}