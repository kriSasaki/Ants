using System;
using UnityEngine;

public class CharacterChanger : ObjectChanger
{
    private const string CurrentItemKey = "CharacterKey";

    [SerializeField] private CharacterDisplay _characterDisplay;
    [SerializeField] private Wallet _wallet;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public int CurrentCharacter { get; private set; }

    private Character _character;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerTransmitter>().Player;
        _interfaceManager = GetComponentInParent<InterfaceManager>();
    }

    private void Start()
    {
        OnLoadDataNeeded?.Invoke(CurrentItemKey, data =>
        {
            CurrentCharacter = data;
        });

        ChangeScriptableObject(CurrentCharacter);
    }

    private void OnEnable()
    {
        _interfaceManager.OnGameStarted += SpawnCharacter;
    }

    private void OnDisable()
    {
        _interfaceManager.OnGameStarted -= SpawnCharacter;
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);
        CurrentCharacter = _currentIndex;
        OnSaveDataNeeded?.Invoke(CurrentItemKey, CurrentCharacter);

        if (_characterDisplay != null)
        {
            _characterDisplay.DisplayCharacter((Character)_scriptableObjects[_currentIndex]);
        }
    }

    public void BuyCharacter()
    {
        _character = (Character)_scriptableObjects[_currentIndex];

        if (_wallet.GoldAmount >= _character.CharacterPrice)
        {
            _character.IsBuyed = true;
            _characterDisplay.DisplayCharacter((Character)_scriptableObjects[_currentIndex]);
            _wallet.ChangeGoldAmount(-_character.CharacterPrice);
        }
    }

    private void SpawnCharacter()
    {
        _character = (Character)_scriptableObjects[CurrentCharacter];
        Instantiate(_character.CharacterModel, _player.transform.position, _player.transform.rotation, _player.gameObject.transform);
        _player.GetHealth(_character.CharacterHealth);
        _player.gameObject.SetActive(true);
    }
}
