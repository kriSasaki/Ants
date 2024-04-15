using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class StartButton : UIElement
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Image _focusImage;
        [SerializeField] private ScaleChanger _scaleChanger;
    
        public event Action OnClick;
    
        private void OnEnable()
        {
            _startButton.onClick.AddListener(ButtonPressed);    
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(ButtonPressed);  
        }

        private void ButtonPressed()
        {
            OnClick?.Invoke();
        }

        public void SetPlayable()
        {
            _buttonImage.color = Color.white;
            _focusImage.color = Color.white;
            _scaleChanger.StartTween();
        }

        public void SetUnplayable()
        {
            _buttonImage.color = Color.gray;
            _focusImage.color = Color.gray;
            _scaleChanger.StopTween();
        }
    }
}
