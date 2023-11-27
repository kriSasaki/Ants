using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _characterName;
    [SerializeField] private TMP_Text _characterPrice;
    [SerializeField] private Transform _characterHolder;
    [SerializeField] private GameObject _playButton;

    public void DisplayCharacter(Character character)
    {
        _characterName.text = character.CharacterName;

        if(character.IsBuyed == false) 
        {
            _characterPrice.gameObject.SetActive(true);
            _characterPrice.text = character.CharacterPrice.ToString();
            _playButton.GetComponent<Image>().color = Color.gray;
            _playButton.GetComponent<Button>().enabled = character.IsBuyed;
        }
        else
        {
            _characterPrice.gameObject.SetActive(false);
            _playButton.GetComponent<Image>().color = Color.white;
            _playButton.GetComponent<Button>().enabled = character.IsBuyed;
        }


        if(_characterHolder.childCount > 0)
        {
            Destroy(_characterHolder.GetChild(0).gameObject);
        }

        Instantiate(character.CharacterModel, _characterHolder.position, _characterHolder.rotation, _characterHolder);
    }
}
