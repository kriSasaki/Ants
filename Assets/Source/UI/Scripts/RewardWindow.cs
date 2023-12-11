using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardWindow : MonoBehaviour
{
    private const string GoldEarned = "Gold earned: ";

    [SerializeField] private PlayerChecker _playerChecker;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<int> _rewards;

    public event Action OnLevelComplete;
    public event Action OnButtonPressed;

    private int _currentLevel;

    private void OnEnable()
    {
        _playerChecker.ConditionIsDone += LevelComplete;
    }

    private void OnDisable()
    {
        _playerChecker.ConditionIsDone -= LevelComplete;
    }

    public void SetCurrentLevel(int currentLevel)
    {
        _currentLevel = currentLevel;
    }

    public void LoadNextLevel()
    {
        OnButtonPressed?.Invoke();
    }

    private void LevelComplete()
    {
        OnLevelComplete?.Invoke();
        _wallet.ChangeGoldAmount(_rewards[_currentLevel]);
        _text.text = GoldEarned + _rewards[_currentLevel].ToString();
    }
}
