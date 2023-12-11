using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    private const string Buyed = "Куплено";

    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterHealth;
    [SerializeField] private TMP_Text _characterPrice;
    [SerializeField] private Transform _rankStars;
    [SerializeField] private GameObject _coin;
    [SerializeField] private Transform _characterHolder;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    public bool ItemIsBuyed { get; private set; }
    public event Action ItemChanged;


    public void DisplayCharacter(Character character)
    {
        _characterName.text = character.CharacterName;
        _characterHealth.text = "Бонус к здоровью: " + character.CharacterHealth.ToString();

        if(character.IsBuyed == false) 
        {
            _characterPrice.GetComponent<Button>().enabled = true;
            _coin.gameObject.SetActive(true);
            _characterPrice.text = character.CharacterPrice.ToString();
            ItemIsBuyed = false;
            ItemChanged?.Invoke();
        }
        else
        {
            _characterPrice.GetComponent<Button>().enabled = false;
            _coin.gameObject.SetActive(false);
            _characterPrice.text = Buyed;
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

    public void ChangeInteractivity(bool isEnable)
    {
        _leftButton.enabled = isEnable;
        _rightButton.enabled = isEnable;
        _characterPrice.gameObject.GetComponent<Button>().enabled = isEnable;
    }
}
