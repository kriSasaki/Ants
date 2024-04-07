using TMPro;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private InterfacePresenter interfacePresenter;
        [SerializeField] private TMP_Text _tmpText;
 
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
            _tmpText.enabled = true;
        }
    }
}
