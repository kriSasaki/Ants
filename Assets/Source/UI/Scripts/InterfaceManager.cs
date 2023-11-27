using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private GameObject _mapButton;
    [SerializeField] private GameObject _mapDisplay;
    [SerializeField] private GameObject _characterDisplay;
    [SerializeField] private GameObject _collectableDisplay;
    [SerializeField] private GameObject _goldDisplay;
    [SerializeField] private GameObject _weaponButton;
    [SerializeField] private GameObject _weaponDisplay;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private float _buttonEnableScale = 1;
    [SerializeField] private float _buttonDisableScale = 0;
    [SerializeField] private float _buttonChangeDuration = 0.4f;

    public event Action OnGameStarted;

    private bool _isPlaying = false;
    private LevelManager _levelManager;

    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        Time.timeScale = 0;
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
            ChangeVisibilityStatus(_weaponButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_characterDisplay, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_mapButton, _buttonDisableScale, _buttonChangeDuration, false);
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

    public void ChangeMapDisplay(bool enable)
    {
        if(enable) 
        {
            ChangeVisibilityStatus(_mapButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_mapDisplay, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_startButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_weaponButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_characterDisplay, _buttonDisableScale, _buttonChangeDuration, false);
        }
        else
        {
            ChangeVisibilityStatus(_mapButton, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_mapDisplay, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_startButton, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_weaponButton, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_characterDisplay, _buttonEnableScale, _buttonChangeDuration, true);
        }
    }

    public void ChangeWeaponDisplay(bool enable)
    {
        if (enable)
        {
            ChangeVisibilityStatus(_weaponButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_weaponDisplay, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_startButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_mapButton, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_characterDisplay, _buttonDisableScale, _buttonChangeDuration, false);
        }
        else
        {
            ChangeVisibilityStatus(_weaponButton, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_weaponDisplay, _buttonDisableScale, _buttonChangeDuration, false);
            ChangeVisibilityStatus(_startButton, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_mapButton, _buttonEnableScale, _buttonChangeDuration, true);
            ChangeVisibilityStatus(_characterDisplay, _buttonEnableScale, _buttonChangeDuration, true);
        }
    }

    private void ChangeVisibilityStatus(GameObject gameObject, float scale, float duration, bool isEnabled)
    {
        gameObject.transform.DOScale(scale, duration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(isEnabled);
        });
    }
}
