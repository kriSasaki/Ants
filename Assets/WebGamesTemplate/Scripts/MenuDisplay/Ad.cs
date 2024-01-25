using System;
using Agava.VKGames;
using UnityEngine;
using Agava.YandexGames;

public class Ad : MonoBehaviour
{
    public event Action<int> Rewarded;
    public event Action VideoOpened;
    public event Action VideoClosed;

    private int _videoReward = 10;

    public void InterestialAdShow()
    {
        InterstitialAd.Show();
    }

    public void VideoAdShow()
    {
        Agava.YandexGames.VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback, OnVideoErrorCallback);
    }

    private void OnVideoOpenCallback()
    {
        VideoOpened?.Invoke();
        Debug.Log("OnVideoOpenCallback");
    }

    private void OnRewardedCallback()
    {
        Rewarded?.Invoke(_videoReward);
        Debug.Log("OnRewardedCallback");
    }

    private void OnVideoCloseCallback()
    {
        Debug.Log("OnVideoCloseCallback");
        VideoClosed?.Invoke();
    }

    private void OnVideoErrorCallback(string message)
    {
        Debug.LogError(message);
    }
}
