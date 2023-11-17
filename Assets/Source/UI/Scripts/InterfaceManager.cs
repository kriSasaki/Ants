using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _RestartButton;

    private bool _isPlaying = false;
    private LevelManager _gameManager;

    private void Start()
    {

        _gameManager = GetComponent<LevelManager>();
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        _isPlaying = true;
        _startButton.SetActive(false);
        _pauseButton.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _startButton.SetActive(true);
        _pauseButton.SetActive(false);
    }

    public void RestartGame()
    {

    }

    public void NextCharacter()
    {

    }

    private void ShowCharacter()
    {

    }
}
