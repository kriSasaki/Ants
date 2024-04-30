using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private InterfacePresenter _interfacePresenter;
        [SerializeField] private TMP_Text _tmpText;

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
            _tmpText.enabled = true;
        }
    }
}