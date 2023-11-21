using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _appearances;
    [SerializeField] private Player _player;

    public int AppearanceAmount => _appearances.Count;
    public int CurentCharacter { get; private set; }
    public event Action OnAppearanceChange;

    private InterfaceManager _interfaceManager;

    private void Awake()
    {
        _interfaceManager = GetComponent<InterfaceManager>();
    }

    private void OnEnable()
    {
        _interfaceManager.OnGameStarted += SpawnCharacter;
    }

    private void OnDisable()
    {
        _interfaceManager.OnGameStarted -= SpawnCharacter;
    }

    public GameObject GetAppearance(int appearanceNumber)
    {
        return _appearances[appearanceNumber];
    }

    public void NextCharacter()
    {
        CurentCharacter++;

        if (CurentCharacter >= _appearances.Count)
        {
            CurentCharacter = 0;
        }
        OnAppearanceChange?.Invoke();
    }

    public void PreviousCharacter()
    {
        CurentCharacter--;

        if (CurentCharacter < 0)
        {
            CurentCharacter = _appearances.Count-1;
        }
        OnAppearanceChange?.Invoke();
    }

    private void SpawnCharacter()
    {
        Instantiate(_appearances[CurentCharacter], _player.transform.position, _player.transform.rotation, _player.gameObject.transform);
        _player.gameObject.SetActive(false);
        _player.gameObject.SetActive(true);
    }
}
