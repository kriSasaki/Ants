using System;
using Source.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class InterfacePresenter : MonoBehaviour
    {
        [SerializeField] private StartButton _startButton;
        [SerializeField] private PauseButton _pauseButton;
        [SerializeField] private UIElement _restartButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private UIElement _collectableDisplay;
        [SerializeField] private UIElement _goldDisplay;
        [SerializeField] private UIElement _buttons;
        [SerializeField] private UIElement _warningWindow;
        [SerializeField] private UIElement _weaponDisplayView;
        [SerializeField] private TimeScaleChanger _timeScaleChanger;
        [SerializeField] private UIElement _mapDisplay;
        [SerializeField] private UIElement _characterDisplayView;
        [SerializeField] private UIElement _rewardWindowView;
        [SerializeField] private RewardWindow _rewardWindow;
        [SerializeField] private WeaponDisplay _weaponDisplay;
        [SerializeField] private CharacterDisplay _characterDisplay;
        
        private bool _isPlayable;
        private bool _isPlaying;

        private void Start()
        {
            _timeScaleChanger.Stop();
        }

        private void OnEnable()
        {
            _startButton.OnClick += OnStartButtonPressed;
            _pauseButton.Clicked += OnPausePressed;
            _continueButton.onClick.AddListener(AcceptWarning);
            _cancelButton.onClick.AddListener(_warningWindow.Hide);
            _weaponDisplay.ItemChanged += CheckPossibilityToPlay;
            _characterDisplay.ItemChanged += CheckPossibilityToPlay;
            _rewardWindow.LevelComplete += ShowRewardWindow;
        }

        private void OnDisable()
        {
            _startButton.OnClick -= OnStartButtonPressed;
            _pauseButton.Clicked -= OnPausePressed;
            _continueButton.onClick.RemoveListener(AcceptWarning);
            _cancelButton.onClick.RemoveListener(_warningWindow.Hide);
            _weaponDisplay.ItemChanged -= CheckPossibilityToPlay;
            _characterDisplay.ItemChanged -= CheckPossibilityToPlay;
            _rewardWindow.LevelComplete -= ShowRewardWindow;
        }

        private void OnStartButtonPressed()
        {
            if (_isPlayable)
            {
                _startButton.SetUnplayable();
                _startButton.Hide();
                _pauseButton.Show();
                _timeScaleChanger.Start();

                if (_isPlaying == false)
                {
                    _isPlaying = true;
                    _mapDisplay.Hide();
                    _characterDisplayView.Hide();
                    _weaponDisplayView.Hide();
                    _collectableDisplay.Show();
                    _goldDisplay.Hide();
                    _buttons.Hide();
                }
                else
                {
                    _restartButton.Hide();
                }
            }
            else
            {
                _warningWindow.Show();
            }
        }

        private void OnPausePressed()
        {
            _timeScaleChanger.Stop();
            _pauseButton.Hide();
            _startButton.SetPlayable();
            _startButton.Show();
            _restartButton.Show();
        }

        private void ShowRewardWindow(bool isLost)
        {
            _rewardWindowView.Show();

            if (isLost)
            {
                _restartButton.Show();
            }
        }

        private void CheckPossibilityToPlay()
        {
            _isPlayable = _weaponDisplay.ItemIsBought && _characterDisplay.ItemIsBought;

            if (_isPlayable)
            {
                _startButton.SetPlayable();
            }
            else
            {
                _startButton.SetUnplayable();
            }
        }

        private void AcceptWarning()
        {
            _warningWindow.Hide();
            _isPlayable = true;
            _startButton.SetPlayable();
            OnStartButtonPressed();
        }
    }
}