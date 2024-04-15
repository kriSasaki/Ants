using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class PauseButton : UIElement
    {
        [SerializeField] private Button _pauseButton; 
    
        public event Action OnClick;
    
        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(ButtonPressed);    
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(ButtonPressed);  
        }

        private void ButtonPressed()
        {
            OnClick?.Invoke();
        }
    }
}