using UnityEngine;

public class CharacterChanger : ScriptableObjectChanger
{
    [SerializeField] private CharacterDisplay _characterDisplay;
    [SerializeField] private Wallet _wallet;

    private Character _character;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerTransmitter>().Player;
        _interfaceManager = GetComponentInParent<InterfaceManager>();
        ChangeScriptableObject(0);
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
        _character = (Character)_scriptableObjects[_currentIndex];
        Instantiate(_character.CharacterModel, _player.transform.position, _player.transform.rotation, _player.gameObject.transform);
        _player.GetHealth(_character.CharacterHealth);
        _player.gameObject.SetActive(true);
    }
}
