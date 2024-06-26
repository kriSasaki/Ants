using UnityEngine;

namespace Source.Scripts.UI
{
    public class MovementTutorial : MonoBehaviour
    {
        [SerializeField] private StartButton _startButton;
        [SerializeField] private GameObject _computerTutorial;
        [SerializeField] private GameObject _mobileTutorial;

        private void OnEnable()
        {
            _startButton.OnClick += ShowTutorial;
        }

        private void OnDisable()
        {
            _startButton.OnClick -= ShowTutorial;
        }

        private void ShowTutorial()
        {
            if (Application.isMobilePlatform)
            {
                _mobileTutorial.SetActive(true);
            }
            else
            {
                _computerTutorial.SetActive(true);
            }
        }
    }
}