using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceVisualizer : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Image _focus;
    [SerializeField] private ScaleChanger _stageActive;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _collectableDisplay;
    [SerializeField] private GameObject _goldDisplay;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private float _enableScale = 1;
    [SerializeField] private float _disableScale = 0;
    [SerializeField] private float _buttonChangeDuration = 0.4f;

    public event Action OnGameStarted;

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
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _weaponDisplay.ItemChanged += CheckPossibilityToPlay;
        _characterDisplay.ItemChanged += CheckPossibilityToPlay;
        _rewardWindow.OnLevelComplete += ShowRewardWindow;
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _weaponDisplay.ItemChanged -= CheckPossibilityToPlay;
        _characterDisplay.ItemChanged -= CheckPossibilityToPlay;
        _rewardWindow.OnLevelComplete -= ShowRewardWindow;
    }

    public void StartGame()
    {
        ChangeVisibilityStatus(_startButton.gameObject, _disableScale, _buttonChangeDuration, false);
        ChangeVisibilityStatus(_pauseButton, _enableScale, _buttonChangeDuration, true);
        Time.timeScale = 1;

        if (_isPlaying == false)
        {
            _isPlaying = true;
            OnGameStarted?.Invoke();
            ChangeVisibilityStatus(_mapDisplay.gameObject, _disableScale, _buttonChangeDuration, false);
            _mapDisplay.ChangeButtonsInteractivity(false);
            ChangeVisibilityStatus(_characterDisplay.gameObject, _disableScale, _buttonChangeDuration, false);
            _characterDisplay.ChangeInteractivity(false);
            ChangeVisibilityStatus(_weaponDisplay.gameObject, _disableScale, _buttonChangeDuration, false);
            _weaponDisplay.ChangeInteractivity(false);
            ChangeVisibilityStatus(_collectableDisplay, _enableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_focus.gameObject, _disableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_goldDisplay, _disableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_buttons, _disableScale, _buttonChangeDuration, false);
        }
        else
        {
            ChangeVisibilityStatus(_restartButton, _disableScale, _buttonChangeDuration, false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        ChangeVisibilityStatus(_startButton.gameObject, _enableScale, _buttonChangeDuration, true);
        ChangeVisibilityStatus(_pauseButton, _disableScale, _buttonChangeDuration, false);
        ChangeVisibilityStatus(_restartButton, _enableScale, _buttonChangeDuration, true);
    }

    private void ShowRewardWindow(bool isLost)
    {
        ChangeVisibilityStatus(_rewardWindow.gameObject, _enableScale, _buttonChangeDuration, true);
        ChangeVisibilityStatus(_restartButton, _enableScale, _buttonChangeDuration, true);
    }

    private void ChangeVisibilityStatus(GameObject gameObject, float scale, float duration, bool isEnable)
    {
        if (isEnable)
        {
            gameObject.SetActive(isEnable);
            gameObject.transform.DOScale(scale, duration).SetEase(Ease.InBack).SetUpdate(true);
        }
        else
        {
            gameObject.transform.DOScale(scale, duration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
            {
                gameObject.SetActive(isEnable);
            });
        }
    }

    private void CheckPossibilityToPlay()
    {
        ChangeActivityStatus(_startButton.gameObject, _weaponDisplay.ItemIsBuyed && _characterDisplay.ItemIsBuyed);

        if (_weaponDisplay.ItemIsBuyed && _characterDisplay.ItemIsBuyed) 
        {
            _focus.color = Color.white;
            _stageActive.StartTween();
        }
        else
        {
            _focus.color = Color.gray;
            _stageActive.StopTween();
        }
    }

    private void ChangeActivityStatus(GameObject gameObject, bool isActive)
    {
        gameObject.GetComponent<Button>().enabled = isActive;

        if (isActive)
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.gray;
        }
    }
}
