using System;
using UnityEngine;

public class CharacterChanger : ObjectChanger
{
    private const string CurrentItemKey = "CharacterKey";

    [SerializeField] private WeaponChanger _weaponChanger;
    [SerializeField] private CharacterDisplay _characterDisplay;
    [SerializeField] private Wallet _wallet;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public event Action ItemBuyed;
    private int CurrentCharacter;

    private Character _character;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerTransmitter>().Player;
        _interfaceVisualizer = GetComponentInParent<InterfaceVisualizer>();
    }

    private void Start()
    {
        for (int i = 0; i < _scriptableObjects.Length; i++)
        {
            OnLoadDataNeeded?.Invoke(CurrentItemKey + i.ToString(), BuyCharacter);
        }

        OnLoadDataNeeded?.Invoke(CurrentItemKey, data =>
        {
            CurrentCharacter = data;
        });

        ChangeScriptableObject(CurrentCharacter);
    }

    private void OnEnable()
    {
        _weaponChanger.ItemBuyed += UpdateDisplay;
        _wallet.GoldAmountChanged += UpdateDisplay;
        _buyButton.onClick.AddListener(TryBuyCharacter);
        _interfaceVisualizer.OnGameStarted += SpawnCharacter;
    }

    private void OnDisable()
    {
        _weaponChanger.ItemBuyed -= UpdateDisplay;
        _wallet.GoldAmountChanged -= UpdateDisplay;
        _buyButton.onClick.RemoveListener(TryBuyCharacter);
        _interfaceVisualizer.OnGameStarted -= SpawnCharacter;
    }

    public override void ChangeScriptableObject(int change)
    {
        base.ChangeScriptableObject(change);
        CurrentCharacter = _currentIndex;
        OnSaveDataNeeded?.Invoke(CurrentItemKey, CurrentCharacter);
        CheckOpportunityToBuy(CurrentCharacter);

        if (_characterDisplay != null)
        {
            _characterDisplay.DisplayCharacter((Character)_scriptableObjects[_currentIndex]);
        }
    }

    private void TryBuyCharacter()
    {
        _character = (Character)_scriptableObjects[CurrentCharacter];

        if (_wallet.GoldAmount >= _character.CharacterPrice)
        {
            BuyCharacter(CurrentCharacter);
            _wallet.ChangeGoldAmount(-_character.CharacterPrice);
            _characterDisplay.DisplayCharacter((Character)_scriptableObjects[CurrentCharacter]);
        }
    }

    private void BuyCharacter(int characterIndex)
    {
        _character = (Character)_scriptableObjects[characterIndex];
        _characterDisplay.ChangePriceAlertStatus(false);
        _character.IsBuyed = true;
        ItemBuyed?.Invoke();
        OnSaveDataNeeded?.Invoke(CurrentItemKey + characterIndex.ToString(), characterIndex);
    }

    private void SpawnCharacter()
    {
        _character = (Character)_scriptableObjects[CurrentCharacter];
        Instantiate(_character.CharacterModel, _player.transform.position, _player.transform.rotation, _player.gameObject.transform);
        _player.GetHealth(_character.CharacterHealth);
        _player.gameObject.SetActive(true);
    }
    
    private void CheckOpportunityToBuy(int index)
    {
        _character = (Character)_scriptableObjects[index];
        _characterDisplay.ChangePriceAlertStatus(_character.CharacterPrice <= _wallet.GoldAmount && _character.IsBuyed == false);

        for (int i = index + 1; i < _scriptableObjects.Length; i++)
        {
            _character = (Character)_scriptableObjects[i];

            if (_character.CharacterPrice <= _wallet.GoldAmount && _character.IsBuyed == false)
            {
                _characterDisplay.ChangeButtonAlertStatus(true);
                break;
            }
            else
            {
                _characterDisplay.ChangeButtonAlertStatus(false);
            }
        }
    }

    private void UpdateDisplay()
    {
        CheckOpportunityToBuy(CurrentCharacter);
    }
}