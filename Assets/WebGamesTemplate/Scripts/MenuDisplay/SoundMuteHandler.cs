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
    [SerializeField] private RewardWindow _rewardWindow;

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
        _ad.VideoOpened += OnVideoOpened;
        _ad.VideoClosed += OnVideoClosed;
        _ad.AdOpened += OnVideoOpened;
        _ad.AdClosed += OnVideoClosed;
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(SoundMuteButtonOn);
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
        _ad.VideoOpened -= OnVideoOpened;
        _ad.VideoClosed -= OnVideoClosed;
        _ad.AdOpened -= OnVideoOpened;
        _ad.AdClosed -= OnVideoClosed;
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
            Debug.Log("OnInBackgroundChange");
            _isPause = inBackground || _isAdActive;
            ChangeAudio(_isPause);
            Time.timeScale = _isPause || _rewardWindow.IsWindowActice ? 0 : 1;
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
        Debug.Log("OnVideoOpened");
        _isAdActive = true;
        
        OnInBackgroundChange(_isAdActive);
    }

    private void ChangeAudio(bool value)
    {
        AudioListener.pause = value;
        AudioListener.volume = value ? 0 : 1; 
    }
}

