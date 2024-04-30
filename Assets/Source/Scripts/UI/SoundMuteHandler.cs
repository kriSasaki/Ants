using System;
using Agava.WebUtility;
using Source.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class SoundMuteHandler : MonoBehaviour
    {
        private const string IsSoundOn = "isSoundOn";

        [SerializeField] private Sprite _mute;
        [SerializeField] private Sprite _unmute;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private AdShower adShower;
        [SerializeField] private RewardWindow _rewardWindow;
        [SerializeField] private TimeScaleChanger _timeScaleChanger;

        private bool _isSoundMute;
        private bool _isAdActive;
        private bool _isPause;

        private void Start()
        {
            if (PlayerPrefs.HasKey(IsSoundOn))
            {
                _isSoundMute = !Convert.ToBoolean(PlayerPrefs.GetInt(IsSoundOn));
            }
            else
            {
                PlayerPrefs.SetInt(IsSoundOn, Convert.ToInt32(true));
                _isSoundMute = false;
            }

            if (_isSoundMute == true)
            {
                ChangeAudio(_isSoundMute);
                _image.sprite = _mute;
            }
            else
            {
                ChangeAudio(_isSoundMute);
                _image.sprite = _unmute;
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SoundMuteButtonOn);
            Application.focusChanged += OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
            adShower.VideoOpened += OnVideoOpened;
            adShower.VideoClosed += OnVideoClosed;
            adShower.AdOpened += OnVideoOpened;
            adShower.AdClosed += OnVideoClosed;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SoundMuteButtonOn);
            Application.focusChanged -= OnInBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
            adShower.VideoOpened -= OnVideoOpened;
            adShower.VideoClosed -= OnVideoClosed;
            adShower.AdOpened -= OnVideoOpened;
            adShower.AdClosed -= OnVideoClosed;
        }

        private void OnInBackgroundChangeApp(bool inBackground)
        {
            OnInBackgroundChange(!inBackground);
        }

        private void OnInBackgroundChangeWeb(bool inBackground)
        {
            OnInBackgroundChange(inBackground);
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            if (_isSoundMute == false)
            {
                _isPause = inBackground || _isAdActive;
                ChangeAudio(_isPause);

                if (_isPause || _rewardWindow.IsWindowActice)
                    _timeScaleChanger.Stop();
                else
                    _timeScaleChanger.Start();
            }
        }

        private void SoundMuteButtonOn()
        {
            if (_isSoundMute == false)
            {
                _isSoundMute = true;
                PlayerPrefs.SetInt(IsSoundOn, Convert.ToInt32(!_isSoundMute));
                _image.sprite = _mute;
                ChangeAudio(_isSoundMute);
            }
            else
            {
                _isSoundMute = false;
                _image.sprite = _unmute;
                PlayerPrefs.SetInt(IsSoundOn, Convert.ToInt32(!_isSoundMute));
                ChangeAudio(_isSoundMute);
            }
        }

        private void OnVideoClosed()
        {
            _isAdActive = false;

            OnInBackgroundChange(_isAdActive);
        }

        private void OnVideoOpened()
        {
            _isAdActive = true;

            OnInBackgroundChange(_isAdActive);
        }

        private void ChangeAudio(bool value)
        {
            AudioListener.pause = value;
            AudioListener.volume = value ? 0 : 1;
        }
    }
}