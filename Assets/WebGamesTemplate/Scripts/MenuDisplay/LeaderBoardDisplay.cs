using Agava.YandexGames;
using System;
using TMPro;
using UnityEngine;

public class LeaderBoardDisplay : MonoBehaviour
{
    private const string Anonymous = "Anonymous";
    private const string PlayerScoreKey = "PlayerScore";

    [SerializeField] private RewardWindow _rewardWindow;
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "LeaderBoard";

    public bool IsAuthorized => PlayerAccount.IsAuthorized;
    public event Action<string, Action<int>> OnLoadDataNeeded;
    public event Action<string, int> OnSaveDataNeeded;

    public int PlayerScore { get; private set; }

    private void Start()
    {
        OnLoadDataNeeded?.Invoke(PlayerScoreKey, data =>
        {
            PlayerScore = data;
        });
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
        if (PlayerAccount.IsAuthorized)
            PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false)
        {
            return;
        }

        Leaderboard.GetEntries(_leaderboardName, (result) =>
        {
            int leadersNumber = result.entries.Length >= _leaderNames.Length ? _leaderNames.Length : result.entries.Length;
            for (int i = 0; i < leadersNumber; i++)
            {
                string name = result.entries[i].player.publicName;

                if (string.IsNullOrEmpty(name))
                {
                    name = Anonymous;
                }

                _leaderNames[i].text = name;
                _scoreList[i].text = result.entries[i].formattedScore;
                _ranks[i].text = result.entries[i].rank.ToString();
            }
        });
    }

    public void SetLeaderboardScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(_leaderboardName, OnSuccessCallback);
        }
    }

    public void Authorize()
    {
        PlayerAccount.Authorize();
    }

    public void SetScore(int score)
    {
        PlayerScore += score;
        OnSaveDataNeeded?.Invoke(PlayerScoreKey, PlayerScore);
    }

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result==null || PlayerScore > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, PlayerScore);
        }      
    }
}
