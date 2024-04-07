using UnityEngine;
using UnityEngine.Serialization;

namespace Source.UI.Scripts
{
    public class MovementTutorial : MonoBehaviour
    {
        [FormerlySerializedAs("_interfaceAnimator")] [SerializeField] private InterfacePresenter interfacePresenter;
        [SerializeField] private GameObject _computerTutorial;
        [SerializeField] private GameObject _mobileTutorial;

        private void OnEnable()
        {
            interfacePresenter.StartButtonPressed += ShowTutorial;
        }

        private void OnDisable()
        {
            interfacePresenter.StartButtonPressed -= ShowTutorial;
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
