using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private StartButton _startButton;
        [SerializeField] private TMP_Text _tmpText;

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
            _tmpText.enabled = true;
        }
    }
}