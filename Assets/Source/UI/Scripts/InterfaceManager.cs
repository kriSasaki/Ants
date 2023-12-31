using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _collectableDisplay;
    [SerializeField] private GameObject _goldDisplay;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private float _enableScale = 1;
    [SerializeField] private float _disableScale = 0;
    [SerializeField] private float _buttonChangeDuration = 0.4f;

    public event Action OnGameStarted;

    private bool _isPlaying = false;
    private LevelManager _levelManager;
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
        _levelManager = GetComponent<LevelManager>();
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _weaponDisplay.ItemChanged += CheckPossibilityToPlay;
        _characterDisplay.ItemChanged += CheckPossibilityToPlay;
        _rewardWindow.OnLevelComplete += ShowRewardWindow;
    }

    private void OnDisable()
    {
        _weaponDisplay.ItemChanged -= CheckPossibilityToPlay;
        _characterDisplay.ItemChanged -= CheckPossibilityToPlay;
        _rewardWindow.OnLevelComplete -= ShowRewardWindow;
    }

    public void StartGame()
    {
        ChangeVisibilityStatus(_startButton, _disableScale, _buttonChangeDuration, false);
        ChangeVisibilityStatus(_pauseButton, _enableScale, _buttonChangeDuration, true);
        _cameraFollower.enabled = true;
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
            ChangeVisibilityStatus(_goldDisplay, _disableScale, _buttonChangeDuration, false);
        }
        else
        {
            ChangeVisibilityStatus(_restartButton, _disableScale, _buttonChangeDuration, false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        ChangeVisibilityStatus(_startButton, _enableScale, _buttonChangeDuration, true);
        ChangeVisibilityStatus(_pauseButton, _disableScale, _buttonChangeDuration, false);
        ChangeVisibilityStatus(_restartButton, _enableScale, _buttonChangeDuration, true);
    }

    private void ShowRewardWindow(bool isLost)
    {
        ChangeVisibilityStatus(_rewardWindow.gameObject, _enableScale, _buttonChangeDuration, true);
        ChangeVisibilityStatus(_restartButton, _enableScale, _buttonChangeDuration, true);
    }

    public void RestartGame()
    {
        _levelManager.LoadLevel(_levelManager.CurrentLevel);
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
        ChangeActivityStatus(_startButton, _weaponDisplay.ItemIsBuyed && _characterDisplay.ItemIsBuyed);
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
