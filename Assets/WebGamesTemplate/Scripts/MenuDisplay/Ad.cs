using System;
using UnityEngine;
using Agava.YandexGames;
using UnityEngine.UI;

public class Ad : MonoBehaviour
{
    [SerializeField] private Button _adButton;
    
    public event Action<int> Rewarded;
    public event Action VideoOpened;
    public event Action VideoClosed;
    public event Action AdOpened;
    public event Action AdClosed;

    private readonly int _videoReward = 10;
    private RewardWindow _rewardWindow;
    private PlayerReviver _playerReviver;

    private void Awake()
    {
        _rewardWindow = GetComponentInChildren<RewardWindow>();
        _playerReviver = GetComponent<PlayerTransmitter>().Player.GetComponent<PlayerReviver>();
    }

    private void OnEnable()
    {
        _adButton.onClick.AddListener(VideoAdShow);
        _rewardWindow.OnNextButtonPressed += InterestialAdShow;
        _rewardWindow.OnRebornButtonPressed += RebornAdShow;
    }

    private void OnDisable()
    {
        _adButton.onClick.RemoveListener(VideoAdShow);
        _rewardWindow.OnNextButtonPressed -= InterestialAdShow;
        _rewardWindow.OnRebornButtonPressed -= RebornAdShow;
    }

    private void InterestialAdShow()
    {
        InterstitialAd.Show(OnOpenCallback, OnCloseCallback);
    }

    private void OnCloseCallback(bool obj)
    {
        AdClosed?.Invoke();
    }

    private void OnOpenCallback()
    {
        AdOpened?.Invoke();
    }

    private void VideoAdShow()
    {
        Agava.YandexGames.VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback);
    }

    private void RebornAdShow()
    {
        Agava.YandexGames.VideoAd.Show(OnVideoOpenCallback,  null, OnVideoCloseCallback);
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
        _playerReviver.RevivePlayer();
        VideoClosed?.Invoke();
    }
}