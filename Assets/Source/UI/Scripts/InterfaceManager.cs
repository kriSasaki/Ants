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

    public event Action OnGameStarted;

    private CharacterDisplayer _characterDisplayer;
    private bool _isPlaying = false;
    private LevelManager _levelManager;

    private void Awake()
    {
        _characterDisplayer = GetComponentInChildren<CharacterDisplayer>();
        Time.timeScale = 0;
    }

    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    public void StartGame()
    {
        _startButton.SetActive(false);
        _pauseButton.SetActive(true);
        _restartButton.SetActive(false);
        Time.timeScale = 1;

        if (_isPlaying == false)
        {
            _characterDisplayer.gameObject.SetActive(false);
            _isPlaying = true;
            OnGameStarted?.Invoke();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _startButton.SetActive(true);
        _pauseButton.SetActive(false);
        _restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        _levelManager.LoadLevel(_levelManager.CurrentLevel);
    }
}
