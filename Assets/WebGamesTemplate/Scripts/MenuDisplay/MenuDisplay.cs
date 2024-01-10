using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDisplay : MonoBehaviour
{
    [SerializeField] private LeaderBoardButton _leaderBoardButton;

    private void Start()
    {
        Show();
    }

    public void Show()
    {
#if YANDEX_GAMES || UNITY_EDITOR
        _leaderBoardButton.gameObject.SetActive(true);
#endif

#if VK_GAMES
        if (Application.isMobilePlatform)
        {
            _leaderBoardButton.gameObject.SetActive(true);
        }
#endif
    }

    public void Hide()
    {
        _leaderBoardButton.gameObject.SetActive(false);
    }
}
