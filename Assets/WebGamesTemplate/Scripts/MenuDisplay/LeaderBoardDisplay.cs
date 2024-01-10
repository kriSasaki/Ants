using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text[] _ranks;
    [SerializeField] private TMP_Text[] _leaderNames;
    [SerializeField] private TMP_Text[] _scoreList;
    [SerializeField] private string _leaderboardName = "LeaderBoard";

    public bool IsAuthorized => PlayerAccount.IsAuthorized;

    private int _playerScore = 69;

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
                    name = "Anonimus";

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

    private void OnSuccessCallback(LeaderboardEntryResponse result)
    {
        if (result==null || _playerScore > result.score)
        {
            Leaderboard.SetScore(_leaderboardName, _playerScore);
        }      
    }
}
