using System;
using Agava.YandexGames;
using Source.Player.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Scripts
{
    public class AdShower : MonoBehaviour
    {
        private const int _videoReward = 10;
        
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
            _rewardWindow.NextButtonPressed += InterestialAdShow;
            _rewardWindow.RebornButtonPressed += RebornAdShow;
        }

        private void OnDisable()
        {
            _adButton.onClick.RemoveListener(VideoAdShow);
            _rewardWindow.NextButtonPressed -= InterestialAdShow;
            _rewardWindow.RebornButtonPressed -= RebornAdShow;
        }

        private void InterestialAdShow()
        {
            InterstitialAd.Show(OnOpenCallback, OnCloseCallback, OnErrorCallback, OnOfflineCallback);
        }

        private void OnOfflineCallback()
        {
            AdClosed?.Invoke();
        }

        private void OnErrorCallback(string obj)
        {
            AdClosed?.Invoke();
        }

        private void OnCloseCallback(bool obj)
        {
            if (obj)
            {
                AdClosed?.Invoke();
            }
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
}