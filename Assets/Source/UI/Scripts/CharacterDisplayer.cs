using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplayer : MonoBehaviour
{
    private CharacterManager _characterManager;
    private List<GameObject> _appearances;

    private void Awake()
    {
        _characterManager = GetComponentInParent<CharacterManager>();
    }

    private void Start()
    {
        SpawnAppearances();
        //ShowAppearance();
    }

    private void SpawnAppearances()
    {
        for (int appearanceNumber = 0; appearanceNumber < _characterManager.AppearanceAmount; appearanceNumber++)
        {
            Debug.Log(_characterManager.GetAppearance(appearanceNumber));
            _appearances.Add(_characterManager.GetAppearance(appearanceNumber));
           _appearances[appearanceNumber] = Instantiate(_appearances[appearanceNumber], transform.position, Quaternion.identity, transform);
        }
    }

    private void ShowAppearance()
    {
        _appearances[_characterManager.CurentCharacter].gameObject.SetActive(true);
    }
}
