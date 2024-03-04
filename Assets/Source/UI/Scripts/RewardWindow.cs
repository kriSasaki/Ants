using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardWindow : MonoBehaviour
{
    private const string Defeat = "Defeat";

    [SerializeField] private Ad _ad;
    [SerializeField] private PlayerChecker _playerChecker;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TMP_Text _earnedGold;
    [SerializeField] private TMP_Text _resultsWin;
    [SerializeField] private TMP_Text _resultsLose;
    [SerializeField] private List<int> _rewards;
    [SerializeField] private Image _ribbon;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Button _rebornButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _labe;
    [SerializeField] private AudioSource _victorySound;
    [SerializeField] private AudioSource _looseSound;

    public bool IsWindowActice { get; private set; } = false;
    public event Action<bool> OnLevelComplete;
    public event Action<int> Rewarded;
    public event Action OnNextButtonPressed;
    public event Action OnRebornButtonPressed;

    private readonly int _defeatDivider = 2;
    private LevelService _levelService;
    private Player _player;
    private bool IsLost = false;

    private void Awake()
    {
        _levelService = GetComponentInParent<LevelService>();
        _player = GetComponentInParent<PlayerTransmitter>().Player;
    }

    private void OnEnable()
    {
        _ad.VideoClosed += CloseRebornButton;
        _nextButton.onClick.AddListener(delegate { OnNextButtonPressed?.Invoke(); });
        _rebornButton.onClick.AddListener(delegate { OnRebornButtonPressed?.Invoke(); });
        _closeButton.onClick.AddListener(LoseLevel);
        _player.OnDeath += LoseLevel;
        _playerChecker.ConditionIsDone += CompleteLevel;
    }

    private void OnDisable()
    {
        _ad.VideoClosed -= CloseRebornButton;
        _nextButton.onClick.RemoveListener(delegate { OnNextButtonPressed?.Invoke(); });
        _rebornButton.onClick.RemoveListener(delegate { OnRebornButtonPressed?.Invoke(); });
        _closeButton.onClick.RemoveListener(LoseLevel);
        _player.OnDeath -= LoseLevel;
        _playerChecker.ConditionIsDone -= CompleteLevel;
    }

    private void CompleteLevel()
    {
        IsWindowActice = true;
        _victorySound.Play();
        Time.timeScale = 0;
        IsLost = false;
        GiveReward(_rewards[_levelService.CurrentLevel]);
        OnLevelComplete?.Invoke(IsLost);
        _resultsWin.enabled = true;
    }

    private void LoseLevel()
    {
        if (IsLost == false)
        {
            IsWindowActice = true;
            Time.timeScale = 0;
            IsLost = true;
            _rebornButton.gameObject.SetActive(IsLost);
        }
        else
        {
            IsWindowActice = true;
            _looseSound.Play();
            _rebornButton.gameObject.SetActive(false);
            Time.timeScale = 0;
            GiveReward(_rewards[_levelService.CurrentLevel] / _defeatDivider);
            SetRedColors();
            _nextButton.gameObject.SetActive(!IsLost);
            OnLevelComplete?.Invoke(IsLost);
            _resultsLose.enabled = true;
        }
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
        _nextButton.GetComponent<Image>().color = Color.red;
        _labe.color = Color.red;
    }

    private void CloseRebornButton()
    {
        IsWindowActice = false;
        _rebornButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}