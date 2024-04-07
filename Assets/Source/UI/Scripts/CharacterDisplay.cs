using System;
using Source.Player.Scripts;
using TMPro;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class CharacterDisplay : ItemDisplay
    {
        private const string BuyedString = "Куплено";
    
        [SerializeField] private TMP_Text _characterHealth;
        [SerializeField] private GameObject _coin;
        [SerializeField] private Transform _characterHolder;
        [SerializeField] private ParticleSystem _particleSystem;

        public bool ItemIsBuyed { get; private set; }
        public event Action ItemChanged;

        private GameObject _appearance;

        public void DisplayCharacter(Character character)
        {
            _characterHealth.text = character.Health.ToString();
            _particleSystem.Stop();
            var main = _particleSystem.main;
            main.startColor = character.Color;
            _particleSystem.Play();
            Buyed.enabled = character.IsBuyed;
            Price.enabled = !character.IsBuyed;

            if (character.IsBuyed == false) 
            {
                BuyButton.enabled = true;
                _coin.gameObject.SetActive(true);
                Price.text = character.Price.ToString();
                ItemIsBuyed = false;
                ItemChanged?.Invoke();
            }
            else
            {
                BuyButton.enabled = false;
                _coin.gameObject.SetActive(false);
                Price.text = Lean.Localization.LeanLocalization.GetTranslationText(BuyedString);
                ItemIsBuyed = true;
                ItemChanged?.Invoke();
            }

            RankStars.ShowStars(character.Rank);

            if (_characterHolder.childCount > 0)
            {
                Destroy(_appearance);
            }

            _appearance = Instantiate(character.Model, _characterHolder.position, _characterHolder.rotation, _characterHolder);
        }
    }
}