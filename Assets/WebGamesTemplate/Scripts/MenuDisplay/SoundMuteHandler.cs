using Agava.WebUtility;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundMuteHandler : MonoBehaviour
{
    private const string IsSoundOn = "isSoundOn";
    
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private Ad _ad;

    private bool _isSoundMute;
    private bool _isAdActive;

    private void OnEnable()
    {
        _button.onClick.AddListener(SoundMuteButtonOn);
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        _ad.VideoOpened += OnVideoOpened;
        _ad.VideoClosed += OnVideoClosed;
        _ad.AdOpened += OnVideoOpened;
        _ad.AdClosed += OnVideoClosed;
    }


    private void OnDisable()
    {
        _button.onClick.RemoveListener(SoundMuteButtonOn);
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        _ad.VideoOpened -= OnVideoOpened;
        _ad.VideoClosed -= OnVideoClosed;
        _ad.AdOpened -= OnVideoOpened;
        _ad.AdClosed -= OnVideoClosed;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        if (!_isSoundMute)
        {
            AudioListener.pause = inBackground;
            AudioListener.pause = _isAdActive;
            AudioListener.volume = inBackground ? 0 : 1; 
        }
    }

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
            DisableSound();
            _image.sprite = _mute;
        }
        else
        {
            EnableSound();
            _image.sprite = _unmute;
        }
    }

    private void SoundMuteButtonOn()
    {
        if (_isSoundMute == false)
        {
            _isSoundMute = true;
            PlayerPrefs.SetInt(IsSoundOn, Convert.ToInt32(!_isSoundMute));
            _image.sprite = _mute;
            DisableSound();
        }
        else
        {
            _isSoundMute = false;
            _image.sprite = _unmute;
            PlayerPrefs.SetInt(IsSoundOn, Convert.ToInt32(!_isSoundMute));
            EnableSound();
        }
    }

    private void OnVideoClosed()
    {
        _isAdActive = false;
        OnInBackgroundChange(_isAdActive);
        
        if (!_isSoundMute)
        {
            EnableSound();
        }
    }

    private void OnVideoOpened()
    {
        DisableSound();
        _isAdActive = true;
    }

    private void EnableSound()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1;
    }

    private void DisableSound()
    {
        AudioListener.pause = true;
        AudioListener.volume = 0;
    }
}

