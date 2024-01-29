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
    private RewardWindow _rewardWindow;

    private void Awake()
    {
        _rewardWindow = GetComponentInChildren<RewardWindow>();
    }

    private void OnEnable()
    {
        _rewardWindow.OnLevelComplete += InterestialAdShow;
    }

    private void OnDisable()
    {
        _rewardWindow.OnLevelComplete -= InterestialAdShow; 
    }

    public void InterestialAdShow(bool isLost)
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
    }

    private void OnRewardedCallback()
    {
        Rewarded?.Invoke(_videoReward);
    }

    private void OnVideoCloseCallback()
    {
        VideoClosed?.Invoke();
    }

    private void OnVideoErrorCallback(string message)
    {
        Debug.LogError(message);
    }
}
