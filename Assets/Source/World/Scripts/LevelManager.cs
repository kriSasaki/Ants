using UnityEngine;
using IJunior.TypedScenes;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

    public int OpenedLevels { get; private set; }
    public int CurrentLevel => _currentLevel;

    private SceneLoadHandler _sceneLoadHandler;
    private RewardWindow _rewardWindow;
    private int _currentLevel = 0;

    private void Awake()
    {
        _rewardWindow = GetComponentInChildren<RewardWindow>();
        _sceneLoadHandler = GetComponent<SceneLoadHandler>();
        _rewardWindow.SetCurrentLevel(CurrentLevel);
    }

    private void OnEnable()
    {
        _rewardWindow.OnButtonPressed += LevelComplete;
    }

    private void OnDisable()
    {
        _rewardWindow.OnButtonPressed -= LevelComplete;
    }

    public void SaveLevels(int currentLevel, int openedLevels)
    {
        _currentLevel = currentLevel;
        OpenedLevels = openedLevels;
    }

    public void LoadLevel(int levelNumber)
    {
        _currentLevel = levelNumber;

        switch(levelNumber)
        {
            case 0:
                Tutorial.Load(_sceneLoadHandler);
                break;
            case 1:
                Level1.Load(_sceneLoadHandler);
                break;
        }
    }

    private void LevelComplete()
    {
        if(OpenedLevels == _currentLevel)
        {
            OpenedLevels++;
        }

        _currentLevel++;
        LoadLevel(_currentLevel);
    }
}
