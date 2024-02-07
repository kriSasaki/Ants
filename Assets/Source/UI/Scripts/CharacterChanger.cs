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
        InterfaceAnimator = GetComponentInParent<InterfaceAnimator>();
    }

    private void Start()
    {
        for (int i = 0; i < _scriptableObjects.Length; i++)
        {
            OnLoadDataNeeded?.Invoke(CurrentItemKey + i.ToString(), data =>
            {
                BuyCharacter(data);
            });
        }

        OnLoadDataNeeded?.Invoke(CurrentItemKey, data =>
        {
            CurrentCharacter = data;
        });

        ChangeScriptableObject(CurrentCharacter);
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(delegate { TryBuyCharacter(CurrentCharacter); });
        InterfaceAnimator.OnGameStarted += SpawnCharacter;
    }

    private void OnDisable()
    {
        _buyButton.onClick.AddListener(delegate { TryBuyCharacter(CurrentCharacter); });
        InterfaceAnimator.OnGameStarted -= SpawnCharacter;
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

    public void TryBuyCharacter(int characterIndex)
    {
        _character = (Character)_scriptableObjects[characterIndex];

        if (_wallet.GoldAmount >= _character.CharacterPrice)
        {
            BuyCharacter(characterIndex);
            _wallet.ChangeGoldAmount(-_character.CharacterPrice);
            _characterDisplay.DisplayCharacter((Character)_scriptableObjects[_currentIndex]);
        }
    }

    private void BuyCharacter(int characterIndex)
    {
        _character = (Character)_scriptableObjects[characterIndex];
        _character.IsBuyed = true;
        OnSaveDataNeeded?.Invoke(CurrentItemKey + characterIndex.ToString(), characterIndex);
    }

    private void SpawnCharacter()
    {
        _character = (Character)_scriptableObjects[CurrentCharacter];
        Instantiate(_character.CharacterModel, _player.transform.position, _player.transform.rotation, _player.gameObject.transform);
        _player.GetHealth(_character.CharacterHealth);
        _player.gameObject.SetActive(true);
    }
}
