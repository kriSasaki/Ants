using System;
using System.Collections.Generic;
using Source.Scripts.World;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class RewardWindow : MonoBehaviour
    {
        private const int DefeatDivider = 2;

        [SerializeField] private AdShower _adShower;
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
        [SerializeField] private TimeScaleChanger _timeScaleChanger;
        [SerializeField] private LevelService _levelService;
        [SerializeField] private Player.Player _player;

        private bool _isLost;

        public event Action<bool> LevelComplete;
        public event Action<int> Rewarded;
        public event Action NextButtonPressed;
        public event Action RebornButtonPressed;

        public bool IsWindowActive { get; private set; }

        private void OnEnable()
        {
            _adShower.VideoClosed += CloseRebornButton;
            _nextButton.onClick.AddListener(ShowAd);
            _rebornButton.onClick.AddListener(Reborn);
            _closeButton.onClick.AddListener(LoseLevel);
            _player.Died += LoseLevel;
            _playerChecker.ConditionIsDone += CompleteLevel;
        }

        private void OnDisable()
        {
            _adShower.VideoClosed -= CloseRebornButton;
            _nextButton.onClick.RemoveListener(ShowAd);
            _rebornButton.onClick.RemoveListener(Reborn);
            _closeButton.onClick.RemoveListener(LoseLevel);
            _player.Died -= LoseLevel;
            _playerChecker.ConditionIsDone -= CompleteLevel;
        }

        private void CompleteLevel()
        {
            IsWindowActive = true;
            _victorySound.Play();
            _timeScaleChanger.Stop();
            _isLost = false;
            GiveReward(_rewards[_levelService.CurrentLevel]);
            LevelComplete?.Invoke(_isLost);
            _resultsWin.enabled = true;
        }

        private void LoseLevel()
        {
            if (_isLost == false)
            {
                IsWindowActive = true;
                _timeScaleChanger.Stop();
                _isLost = true;
                _rebornButton.gameObject.SetActive(_isLost);
            }
            else
            {
                IsWindowActive = true;
                _looseSound.Play();
                _rebornButton.gameObject.SetActive(false);
                _timeScaleChanger.Stop();
                GiveReward(_rewards[_levelService.CurrentLevel] / DefeatDivider);
                SetRedColors();
                _nextButton.gameObject.SetActive(!_isLost);
                LevelComplete?.Invoke(_isLost);
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
            IsWindowActive = false;
            _rebornButton.gameObject.SetActive(false);
            _timeScaleChanger.Start();
        }

        private void ShowAd()
        {
            NextButtonPressed?.Invoke();
        }

        private void Reborn()
        {
            RebornButtonPressed?.Invoke();
        }
    }
}