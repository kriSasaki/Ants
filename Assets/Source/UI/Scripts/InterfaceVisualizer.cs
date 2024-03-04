using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceVisualizer : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Image _focus;
    [SerializeField] private ScaleChanger _stageActive;
    [SerializeField] private GameObject _collectableDisplay;
    [SerializeField] private GameObject _goldDisplay;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _warningWindow;
    [SerializeField] private float _enableScale = 1;
    [SerializeField] private float _disableScale = 0;
    [SerializeField] private float _changeDuration = 0.4f;

    public event Action OnGameStarted;

    private bool _isPlayable = false;
    private bool _isPlaying = false;
    private WeaponDisplay _weaponDisplay;
    private MapChanger _mapDisplay;
    private CharacterDisplay _characterDisplay;
    private RewardWindow _rewardWindow;

    private void Awake()
    {
        _weaponDisplay = GetComponentInChildren<WeaponDisplay>();
        _mapDisplay = GetComponentInChildren<MapChanger>();
        _characterDisplay = GetComponentInChildren<CharacterDisplay>();
        _rewardWindow = GetComponentInChildren<RewardWindow>();
    }

    private void Start()
    {
        Time.timeScale = 0;
        _stageActive.StartTween();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _continueButton.onClick.AddListener(AcceptWarning);
        _cancelButton.onClick.AddListener(delegate { ChangeVisibilityStatus(new[] { _warningWindow }); });
        _pauseButton.onClick.AddListener(PauseGame);
        _weaponDisplay.ItemChanged += CheckPossibilityToPlay;
        _characterDisplay.ItemChanged += CheckPossibilityToPlay;
        _rewardWindow.OnLevelComplete += ShowRewardWindow;
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _continueButton.onClick.RemoveListener(AcceptWarning);
        _cancelButton.onClick.RemoveListener(delegate { ChangeVisibilityStatus(new[] { _warningWindow }); });
        _pauseButton.onClick.RemoveListener(PauseGame);
        _weaponDisplay.ItemChanged -= CheckPossibilityToPlay;
        _characterDisplay.ItemChanged -= CheckPossibilityToPlay;
        _rewardWindow.OnLevelComplete -= ShowRewardWindow;
    }

    private void StartGame()
    {
        if (_isPlayable)
        {
            ChangeVisibilityStatus(new[] { _startButton.gameObject, _pauseButton.gameObject });
            _startButton.enabled = false;
            _pauseButton.enabled = true;
            _stageActive.StopTween();
            Time.timeScale = 1;

            if (_isPlaying == false)
            {
                _isPlaying = true;
                OnGameStarted?.Invoke();
                ChangeVisibilityStatus(new[]
                {
                    _mapDisplay.gameObject, _characterDisplay.gameObject, _weaponDisplay.gameObject,
                    _collectableDisplay, _focus.gameObject, _goldDisplay, _buttons
                });

                _mapDisplay.ChangeButtonsInteractivity(false);
                _characterDisplay.ChangeInteractivity(false);
                _weaponDisplay.ChangeInteractivity(false);
                _stageActive.StopTween();
            }
            else
            {
                ChangeVisibilityStatus(new[] { _restartButton.gameObject });
            }
        }
        else
        {
            ChangeVisibilityStatus(new[] { _warningWindow });
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _pauseButton.enabled = false;
        _startButton.enabled = true;
        _restartButton.enabled = true;
        ChangeVisibilityStatus(new[] { _startButton.gameObject, _pauseButton.gameObject, _restartButton.gameObject });
    }

    private void ShowRewardWindow(bool isLost)
    {
        _rewardWindow.gameObject.SetActive(false);
        ChangeVisibilityStatus(new[] { _rewardWindow.gameObject });

        if (isLost)
        {
            ChangeVisibilityStatus(new[] { _restartButton.gameObject });
        }
    }

    private void ChangeVisibilityStatus(GameObject[] gameObjects)
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject.activeSelf)
            {
                gameObject.transform.DOScale(_disableScale, _changeDuration).SetEase(Ease.InBack).SetUpdate(true)
                    .OnComplete(() => { gameObject.SetActive(false); });
            }
            else
            {
                gameObject.SetActive(true);
                gameObject.transform.DOScale(_enableScale, _changeDuration).SetEase(Ease.InBack).SetUpdate(true);
            }
        }
    }

    private void CheckPossibilityToPlay()
    {
        _isPlayable = _weaponDisplay.ItemIsBuyed && _characterDisplay.ItemIsBuyed;

        if (_isPlayable)
        {
            _startButton.GetComponent<Image>().color = Color.white;
            _focus.color = Color.white;
            _stageActive.StartTween();
        }
        else
        {
            _startButton.GetComponent<Image>().color = Color.gray;
            _focus.color = Color.gray;
            _stageActive.StopTween();
        }
    }

    private void AcceptWarning()
    {
        ChangeVisibilityStatus(new[] { _warningWindow });
        _isPlayable = true;
        _startButton.GetComponent<Image>().color = Color.white;
        _focus.color = Color.white;
        StartGame();
    }
}
