using System;
using Agava.YandexGames;
using Source.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class AdShower : MonoBehaviour
    {
        private const int VideoReward = 10;

        [SerializeField] private Button _adButton;
        [SerializeField] private RewardWindow _rewardWindow;
        [SerializeField] private PlayerReviver _playerReviver;

        public event Action<int> Rewarded;
        public event Action VideoOpened;
        public event Action VideoClosed;
        public event Action AdOpened;
        public event Action AdClosed;


        private void OnEnable()
        {
            _adButton.onClick.AddListener(VideoAdShow);
            _rewardWindow.NextButtonPressed += InterstitialAdShow;
            _rewardWindow.RebornButtonPressed += RebornAdShow;
        }

        private void OnDisable()
        {
            _adButton.onClick.RemoveListener(VideoAdShow);
            _rewardWindow.NextButtonPressed -= InterstitialAdShow;
            _rewardWindow.RebornButtonPressed -= RebornAdShow;
        }

        private void InterstitialAdShow()
        {
            InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback, OnOfflineCallback);
        }

        private void OnOfflineCallback()
        {
            AdClosed?.Invoke();
        }

        private void OnErrorCallback(string errorMessage)
        {
            AdClosed?.Invoke();
        }

        private void OnCloseCallback(bool isClosed)
        {
            if (isClosed) AdClosed?.Invoke();
        }

        private void OnOpenCallback()
        {
            AdOpened?.Invoke();
        }

        private void VideoAdShow()
        {
            VideoAd.Show(OnVideoOpenCallback, OnRewardedCallback, OnVideoCloseCallback);
        }

        private void RebornAdShow()
        {
            VideoAd.Show(OnVideoOpenCallback, null, OnVideoCloseCallback);
        }

        private void OnVideoOpenCallback()
        {
            VideoOpened?.Invoke();
        }

        private void OnRewardedCallback()
        {
            Rewarded?.Invoke(VideoReward);
        }

        private void OnVideoCloseCallback()
        {
            _playerReviver.RevivePlayer();
            VideoClosed?.Invoke();
        }
    }
}