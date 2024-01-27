using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private const string OpenedLevelsKey = "OpenedLevels";
    private const string CurrentLevelKey = "CurrentLevel";
    private readonly string[] _keys = { OpenedLevelsKey, CurrentLevelKey };

    [SerializeField] private PlayerChecker _playerChecker;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public event Action OnLevelLoaded;
    public int OpenedLevels { get; private set; }
    public int CurrentLevel => _currentLevel;
    private RewardWindow _rewardWindow;
    private int _currentLevel;

    private void Awake()
    {
        _rewardWindow = GetComponentInChildren<RewardWindow>();
        _rewardWindow.SetCurrentLevel(CurrentLevel);
    }

    private void Start()
    {
        foreach (var key in _keys)
        {
            OnLoadDataNeeded?.Invoke(key, data =>
            {
                switch (key)
                {
                    case OpenedLevelsKey:
                        OpenedLevels = data;
                        break;
                    case CurrentLevelKey:
                        _currentLevel = data;
                        break;
                }
            });
        }

        OnLevelLoaded?.Invoke();
    }

    private void OnEnable()
    {
        _rewardWindow.OnLevelComplete += LevelComplete;
        _rewardWindow.OnButtonPressed += LoadNextLevel;
    }

    private void OnDisable()
    {
        _rewardWindow.OnLevelComplete -= LevelComplete;
        _rewardWindow.OnButtonPressed -= LoadNextLevel;
    }

    private void SaveLevels(int currentLevel, int openedLevels)
    {
        _currentLevel = currentLevel;
        OpenedLevels = openedLevels;
    }

    public void LoadLevel(int levelNumber)
    {
        _currentLevel = levelNumber;
        OnSaveDataNeeded?.Invoke(CurrentLevelKey, _currentLevel);

        switch (levelNumber)
        {
            case (int)SceneName.Tutorial:
                SceneManager.LoadScene(SceneName.Tutorial.ToString());
                break;
            case (int)SceneName.Level1:
                SceneManager.LoadScene(SceneName.Level1.ToString());
                break;
            case (int)SceneName.Level2:
                SceneManager.LoadScene(SceneName.Level2.ToString());
                break;
        }
    }

    private void LevelComplete(bool isLost)
    {
        if (!isLost && OpenedLevels == _currentLevel)
        {
            OpenedLevels++;
            OnSaveDataNeeded?.Invoke(OpenedLevelsKey, OpenedLevels);
        }
    }

    private void LoadNextLevel()
    {
        _currentLevel++;
        OnSaveDataNeeded?.Invoke(CurrentLevelKey, _currentLevel);
        LoadLevel(_currentLevel);
    }

    private enum SceneName
    {
        Tutorial,
        Level1,
        Level2
    }
}