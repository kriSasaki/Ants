using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardButton : MonoBehaviour
{
    [SerializeField] private GameObject _authorizationWindow;
    [SerializeField] private Button _authorizeButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private Button _button;
    [SerializeField] private LeaderBoardDisplay _leaderBoardDisplay;
    [SerializeField] private Button _closeButton;

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
        if (_leaderBoardDisplay.transform.localScale == new Vector3(1.0f, 1.0f, 1.0f))
        {
            Hide();
        }
        else 
        {
            Show();
        }
    }

    private void Show()
    {
        if (_leaderBoardDisplay.IsAuthorized == false)
        {
            //_authorizationWindow.SetActive(true);
            _authorizeButton.onClick.AddListener(_leaderBoardDisplay.Authorize);
            _cancelButton.onClick.AddListener(CloseAuthorization);
        }
        else
        {
            _leaderBoardDisplay.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
            //_leaderBoardDisplay.gameObject.SetActive(true);
            _leaderBoardDisplay.SetLeaderboardScore();
            _leaderBoardDisplay.OpenYandexLeaderboard();
        }
    }

    private void Hide()
    {
        //_leaderBoardDisplay.gameObject.SetActive(false);
        _leaderBoardDisplay.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    private void CloseAuthorization()
    {
        _authorizeButton.onClick.RemoveListener(_leaderBoardDisplay.Authorize);
        _cancelButton.onClick.RemoveListener(CloseAuthorization);
        _authorizationWindow.SetActive(false);
    }
}
