using UnityEngine;

namespace Source.Scripts.UI
{
    public class MovementTutorial : MonoBehaviour
    {
        [SerializeField] private InterfacePresenter _interfacePresenter;
        [SerializeField] private GameObject _computerTutorial;
        [SerializeField] private GameObject _mobileTutorial;

        private void OnEnable()
        {
            _interfacePresenter.StartButtonPressed += ShowTutorial;
        }

        private void OnDisable()
        {
            _interfacePresenter.StartButtonPressed -= ShowTutorial;
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
