using Source.Scripts.World;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class LeaderBoardButton : MonoBehaviour
    {
        private readonly Vector3 _enabledSize = new(1f, 1f, 1f);
        private readonly Vector3 _disabledSize = new(0f, 0f, 0f);

        [SerializeField] private GameObject _authorizationWindow;
        [SerializeField] private Button _authorizeButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _button;
        [SerializeField] private LeaderBoardDisplay _leaderBoardDisplay;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TimeScaleChanger _timeScaleChanger;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
            _closeButton.onClick.AddListener(OnCloseClock);
            _leaderBoardDisplay.SetLeaderboardScore();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _closeButton.onClick.RemoveListener(OnCloseClock);
        }

        private void OnCloseClock()
        {
            Hide();
        }

        private void OnClick()
        {
            if (_leaderBoardDisplay.transform.localScale == _enabledSize)
                Hide();
            else
                Show();
        }

        private void Show()
        {
            if (_leaderBoardDisplay.IsAuthorized == false)
            {
                _timeScaleChanger.Stop();
                _authorizationWindow.SetActive(true);
                _cancelButton.onClick.AddListener(CloseAuthorization);
                _authorizeButton.onClick.AddListener(_leaderBoardDisplay.Authorize);
                _authorizeButton.onClick.AddListener(CloseAuthorization);
            }
            else
            {
                _leaderBoardDisplay.transform.localScale += _enabledSize;
                _leaderBoardDisplay.SetLeaderboardScore();
                _leaderBoardDisplay.OpenYandexLeaderboard();
            }
        }

        private void Hide()
        {
            _leaderBoardDisplay.transform.localScale = _disabledSize;
        }

        private void CloseAuthorization()
        {
            _authorizeButton.onClick.RemoveListener(_leaderBoardDisplay.Authorize);
            _authorizeButton.onClick.RemoveListener(CloseAuthorization);
            _cancelButton.onClick.RemoveListener(CloseAuthorization);
            _authorizationWindow.SetActive(false);
        }
    }
}