using System;
using Source.UI.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Source.World.Scripts
{
    public class LevelService : MonoBehaviour
    {
        private const string OpenedLevelsKey = "OpenedLevels";
        private const string CurrentLevelKey = "CurrentLevel";
        private readonly string[] _keys = { OpenedLevelsKey, CurrentLevelKey };

        [SerializeField] private RewardWindow _rewardWindow;
        [SerializeField] private AdShower adShower;
        [SerializeField] private Button _restartButton;

        public event Action<string, Action<int>> LoadDataNeeded;
        public event Action<string, int> SaveDataNeeded;
        public event Action OnLevelLoaded;
        public int OpenedLevels { get; private set; }
        public int CurrentLevel { get; private set; }

        private void Start()
        {
            foreach (var key in _keys)
            {
                LoadDataNeeded?.Invoke(key, data =>
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
            _rewardWindow.LevelComplete += LevelComplete;
            adShower.AdClosed += LoadNextLevel;
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(RestartGame);
            _rewardWindow.LevelComplete -= LevelComplete;
            adShower.AdClosed -= LoadNextLevel;
        }

        public void LoadLevel(int levelNumber)
        {
            CurrentLevel = levelNumber;
            SaveDataNeeded?.Invoke(CurrentLevelKey, CurrentLevel);

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
                case (int)SceneName.Level6:
                    SceneManager.LoadScene(SceneName.Level6.ToString());
                    break;
                case (int)SceneName.Level7:
                    SceneManager.LoadScene(SceneName.Level7.ToString());
                    break;
            }
        }
    
        private void RestartGame()
        {
            LoadLevel(CurrentLevel);
        }

        private void LevelComplete(bool isLost)
        {
            if (!isLost && OpenedLevels == CurrentLevel && OpenedLevels != (int)SceneName. Level7)
            {
                OpenedLevels++;
                SaveDataNeeded?.Invoke(OpenedLevelsKey, OpenedLevels);
            }
        }

        private void LoadNextLevel()
        {
            if (CurrentLevel != (int)SceneName.Level7)
            {
                CurrentLevel++;
                SaveDataNeeded?.Invoke(CurrentLevelKey, CurrentLevel);
            }
        
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
}