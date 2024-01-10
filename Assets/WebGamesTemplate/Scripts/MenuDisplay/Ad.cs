using System;
using Agava.VKGames;
using UnityEngine;
using Agava.YandexGames;

public class Ad : MonoBehaviour
{
    public event Action Rewarded;
    public event Action VideoOpened;
    public event Action VideoClosed;

    public void InterestialAdShow()
    {
#if YANDEX_GAMES
        InterstitialAd.Show();
#endif

#if VK_GAMES
        Interstitial.Show();
#endif
    }

    public void VideoAdShow()
    {
#if UNITY_EDITOR
        OnRewardedCallback();
        OnVideoCloseCallback();
        return;
#endif

#if YANDEX_GAMES
        Agava.YandexGames.VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback, OnVideoErrorCallback);
#endif

#if VK_GAMES
        Agava.VKGames.VideoAd.Show(OnRewardedCallback);
#endif
    }

    private void OnVideoOpenCallback()
    {
        VideoOpened?.Invoke();
    }

    private void OnVideoCloseCallback()
    {
        VideoClosed?.Invoke();
    }

    private void OnRewardedCallback()
    {
        Rewarded?.Invoke();
    }

    private void OnVideoErrorCallback(string message)
    {
        Debug.LogError(message);
    }
}
