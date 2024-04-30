using System;
using Source.Scripts.Player;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class CharacterDisplay : ItemDisplay
    {
        private const string BoughtString = "Куплено";

        [SerializeField] private TMP_Text _characterHealth;
        [SerializeField] private GameObject _coin;
        [SerializeField] private Transform _characterHolder;
        [SerializeField] private ParticleSystem _particleSystem;

        public bool ItemIsBought { get; private set; }

        public event Action ItemChanged;

        private GameObject _appearance;

        public void DisplayCharacter(Character character)
        {
            _characterHealth.text = character.Health.ToString();
            _particleSystem.Stop();
            var main = _particleSystem.main;
            main.startColor = character.Color;
            _particleSystem.Play();
            Bought.enabled = character.IsBought;
            Price.enabled = !character.IsBought;

            if (character.IsBought == false)
            {
                BuyButton.enabled = true;
                _coin.gameObject.SetActive(true);
                Price.text = character.Price.ToString();
                ItemIsBought = false;
                ItemChanged?.Invoke();
            }
            else
            {
                BuyButton.enabled = false;
                _coin.gameObject.SetActive(false);
                Price.text = Lean.Localization.LeanLocalization.GetTranslationText(BoughtString);
                ItemIsBought = true;
                ItemChanged?.Invoke();
            }

            RankStars.ShowStars(character.Rank);

            if (_characterHolder.childCount > 0) Destroy(_appearance);

            _appearance = Instantiate(character.Model, _characterHolder.position, _characterHolder.rotation,
                _characterHolder);
        }
    }
}