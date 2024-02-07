using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardWindow : MonoBehaviour
{
    private const string Defeat = "Defeat";

    [SerializeField] private PlayerChecker _playerChecker;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _results;
    [SerializeField] private List<int> _rewards;
    [SerializeField] private Image _ribbon;
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _labe;

    public event Action<bool> OnLevelComplete;
    public event Action<int> Rewarded;
    public event Action OnButtonPressed;

    private LevelService _levelService;
    private Player _player;
    private bool IsLost;
    private int _currentLevel;
    private int _defeatDivider = 2;

    private void Awake()
    {
        _levelService = GetComponentInParent<LevelService>();
        _player = GetComponentInParent<PlayerTransmitter>().Player;
    }

    private void OnEnable()
    {
        _player.OnDeath += LoseLevel;
        _playerChecker.ConditionIsDone += CompleteLevel;
    }

    private void OnDisable()
    {
        _player.OnDeath -= LoseLevel;
        _playerChecker.ConditionIsDone -= CompleteLevel;
    }

    public void SetCurrentLevel(int currentLevel)
    {
        _currentLevel = currentLevel;
    }

    public void LoadNextLevel()
    {
        OnButtonPressed?.Invoke();
    }

    private void CompleteLevel()
    {
        Time.timeScale = 0;
        IsLost = false;
        GiveReward(_rewards[_levelService.CurrentLevel]);
        OnLevelComplete?.Invoke(IsLost);
    }

    private void LoseLevel()
    {
        Time.timeScale = 0;
        IsLost = true;
        GiveReward(_rewards[_levelService.CurrentLevel] / _defeatDivider);
        SetRedColors();
        _results.text = Defeat;
        _button.SetActive(!IsLost);
        OnLevelComplete?.Invoke(IsLost);
    }

    private void GiveReward(int reward)
    {
        Rewarded?.Invoke(reward);
        _wallet.ChangeGoldAmount(reward);
        _earnedGold.text = reward.ToString();
    }

    private void SetRedColors()
    {
        _ribbon.color = Color.red;
        _button.GetComponent<Image>().color = Color.red;
        _labe.color = Color.red;
    }
}
