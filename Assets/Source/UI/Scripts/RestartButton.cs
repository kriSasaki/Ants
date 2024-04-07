using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Scripts
{
    public class RestartButton : UIElement
    {
        [SerializeField] private Button _restartButton; 
    
        public event Action OnClick;
    
        private void OnEnable()
        {
            _restartButton.onClick.AddListener(ButtonPressed);    
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(ButtonPressed);  
        }

        private void ButtonPressed()
        {
            OnClick?.Invoke();
        }
    }
}
