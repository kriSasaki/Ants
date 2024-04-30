using System;
using Agava.YandexGames;
using Source.Scripts.World;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class LeaderBoardDisplay : MonoBehaviour
    {
        private const string Anonymous = "Anonymous";
        private const string PlayerScoreKey = "PlayerScore";

        [SerializeField] private RewardWindow _rewardWindow;
        [SerializeField] private TMP_Text[] _ranks;
        [SerializeField] private TMP_Text[] _leaderNames;
        [SerializeField] private TMP_Text[] _scoreList;
        [SerializeField] private string _leaderboardName = "LeaderBoard";
        [SerializeField] private TimeScaleChanger _timeScaleChanger;

        public bool IsAuthorized => PlayerAccount.IsAuthorized;

        public event Action<string, Action<int>> LoadDataNeeded;
        public event Action<string, int> SaveDataNeeded;

        private int _playerScore;

        private int _leadersNumber;

        private void Start()
        {
            LoadDataNeeded?.Invoke(PlayerScoreKey, data => { _playerScore = data; });
        }

        private void OnEnable()
        {
            _rewardWindow.Rewarded += SetScore;
        }

        private void OnDisable()
        {
            _rewardWindow.Rewarded -= SetScore;
        }

        public void OpenYandexLeaderboard()
        {
            if (PlayerAccount.IsAuthorized == false) return;

            Leaderboard.GetEntries(_leaderboardName, (result) =>
            {
                _leadersNumber = result.entries.Length >= _leaderNames.Length
                    ? _leaderNames.Length
                    : result.entries.Length;

                for (var i = 0; i < _leadersNumber; i++)
                {
                    var name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                    {
                        name = Anonymous;
                        name = Lean.Localization.LeanLocalization.GetTranslationText(name);
                    }

                    _leaderNames[i].text = name;
                    _scoreList[i].text = result.entries[i].formattedScore;
                    _ranks[i].text = result.entries[i].rank.ToString();
                }
            });
        }

        public void SetLeaderboardScore()
        {
            if (YandexGamesSdk.IsInitialized) Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }

        public void Authorize()
        {
            PlayerAccount.Authorize(OnSuccessCallback, OnErrorCallback);
        }

        private void OnErrorCallback(string obj)
        {
            _timeScaleChanger.Start();
        }

        private void OnSuccessCallback()
        {
            _timeScaleChanger.Start();
        }

        private void SetScore(int score)
        {
            _playerScore += score;
            SaveDataNeeded?.Invoke(PlayerScoreKey, _playerScore);
        }

        private void OnSuccessCallback(LeaderboardEntryResponse result)
        {
            if (result == null || _playerScore > result.score) Leaderboard.SetScore(_leaderboardName, _playerScore);
        }
    }
}