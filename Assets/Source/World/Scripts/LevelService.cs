using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelService : MonoBehaviour
{
    private const string OpenedLevelsKey = "OpenedLevels";
    private const string CurrentLevelKey = "CurrentLevel";
    private readonly string[] _keys = { OpenedLevelsKey, CurrentLevelKey };

    [SerializeField] private PlayerChecker _playerChecker;
    [SerializeField] private Button _restartButton;

    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;
    public event Action OnLevelLoaded;
    public int OpenedLevels { get; private set; }
    public int CurrentLevel { get; private set; }
    private RewardWindow _rewardWindow;

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
                        CurrentLevel = data;
                        break;
                }
            });
        }

        OnLevelLoaded?.Invoke();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _rewardWindow.OnLevelComplete += LevelComplete;
        _rewardWindow.OnButtonPressed += LoadNextLevel;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _rewardWindow.OnLevelComplete -= LevelComplete;
        _rewardWindow.OnButtonPressed -= LoadNextLevel;
    }

    public void LoadLevel(int levelNumber)
    {
        CurrentLevel = levelNumber;
        OnSaveDataNeeded?.Invoke(CurrentLevelKey, CurrentLevel);

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
            case (int)SceneName.Level3:
                SceneManager.LoadScene(SceneName.Level3.ToString());
                break;
            case (int)SceneName.Level4:
                SceneManager.LoadScene(SceneName.Level4.ToString());
                break;
            case (int)SceneName.Level5:
                SceneManager.LoadScene(SceneName.Level5.ToString());
                break;
        }
    }
    
    private void RestartGame()
    {
        LoadLevel(CurrentLevel);
    }

    private void LevelComplete(bool isLost)
    {
        if (!isLost && OpenedLevels == CurrentLevel)
        {
            OpenedLevels++;
            OnSaveDataNeeded?.Invoke(OpenedLevelsKey, OpenedLevels);
        }
    }

    private void LoadNextLevel()
    {
        CurrentLevel++;
        OnSaveDataNeeded?.Invoke(CurrentLevelKey, CurrentLevel);
        LoadLevel(CurrentLevel);
    }

    private enum SceneName
    {
        Tutorial,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7
    }
}