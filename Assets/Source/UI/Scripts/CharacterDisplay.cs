using System;
using TMPro;
using UnityEngine;

public class CharacterDisplay : ItemDisplay
{
    private const string Buyed = "Куплено";

    [SerializeField] private TMP_Text _characterHealth;
    [SerializeField] private Transform _rankStars;
    [SerializeField] private GameObject _coin;
    [SerializeField] private Transform _characterHolder;
    [SerializeField] private ParticleSystem _particleSystem;

    public bool ItemIsBuyed { get; private set; }
    public event Action ItemChanged;

    public void DisplayCharacter(Character character)
    {
        _characterHealth.text = character.CharacterHealth.ToString();
        _particleSystem.Stop();
        var main = _particleSystem.main;
        main.startColor = character.Color;
        _particleSystem.Play();
        _buyed.enabled = character.IsBuyed;
        _price.enabled = !character.IsBuyed;

        if (character.IsBuyed == false) 
        {
            _buyButton.enabled = true;
            _coin.gameObject.SetActive(true);
            _price.text = character.CharacterPrice.ToString();
            ItemIsBuyed = false;
            ItemChanged?.Invoke();
        }
        else
        {
            _buyButton.enabled = false;
            _coin.gameObject.SetActive(false);
            _price.text = Lean.Localization.LeanLocalization.GetTranslationText(Buyed);
            ItemIsBuyed = true;
            ItemChanged?.Invoke();
        }

        for (int star = 0; star < _rankStars.childCount; star++)
        {
            _rankStars.GetChild(star).GetChild(0).gameObject.SetActive(star < character.CharacterRank);
        }

        if (_characterHolder.childCount > 0)
        {
            Destroy(_characterHolder.GetChild(0).gameObject);
        }

        Instantiate(character.CharacterModel, _characterHolder.position, _characterHolder.rotation, _characterHolder);
    }
}