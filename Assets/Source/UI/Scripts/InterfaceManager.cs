using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _mapDisplay;
    [SerializeField] private CharacterDisplay _characterDisplay;
    [SerializeField] private GameObject _collectableDisplay;
    [SerializeField] private GameObject _goldDisplay;
    [SerializeField] private WeaponDisplay _weaponDisplay;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private float _buttonEnableScale = 1;
    [SerializeField] private float _buttonDisableScale = 0;
    [SerializeField] private float _buttonChangeDuration = 0.4f;

    public event Action OnGameStarted;

    private bool _isPlaying = false;
    private LevelManager _levelManager;
    private MapDisplay _mapIcon;

    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _weaponDisplay.ItemChanged += CheckPossibilityToPlay;
        _characterDisplay.ItemChanged += CheckPossibilityToPlay;
    }

    private void OnDisable()
    {
        _weaponDisplay.ItemChanged -= CheckPossibilityToPlay;
        _characterDisplay.ItemChanged -= CheckPossibilityToPlay;
    }

    public void StartGame()
    {
        ChangeVisibilityStatus(_startButton, _buttonDisableScale, _buttonChangeDuration, false);
        ChangeVisibilityStatus(_pauseButton, _buttonEnableScale, _buttonChangeDuration, true);
        _cameraFollower.enabled = true;
        Time.timeScale = 1;

        if (_isPlaying == false)
        {
            _isPlaying = true;
            OnGameStarted?.Invoke();
            ChangeVisibilityStatus(_mapDisplay, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_characterDisplay.gameObject, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_weaponDisplay.gameObject, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_collectableDisplay, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_goldDisplay, _buttonDisableScale, _buttonChangeDuration, false);
        }
        else
        {
            ChangeVisibilityStatus(_restartButton, _buttonDisableScale, _buttonChangeDuration, false);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        ChangeVisibilityStatus(_startButton, _buttonEnableScale, _buttonChangeDuration, true);
        ChangeVisibilityStatus(_pauseButton, _buttonDisableScale, _buttonChangeDuration, false);
        ChangeVisibilityStatus(_restartButton, _buttonEnableScale, _buttonChangeDuration, true);
    }

    public void RestartGame()
    {
        _levelManager.LoadLevel(_levelManager.CurrentLevel);
    }

    private void ChangeVisibilityStatus(GameObject gameObject, float scale, float duration, bool isEnabled)
    {
        gameObject.transform.DOScale(scale, duration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(isEnabled);
        });
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
