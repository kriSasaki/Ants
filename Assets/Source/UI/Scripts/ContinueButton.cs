using System;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Scripts
{
    public class ContinueButton : UIElement
    {
        [SerializeField] private Button _continueButton; 
    
        public event Action OnClick;
    
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ButtonPressed);    
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ButtonPressed);  
        }

        private void ButtonPressed()
        {
            OnClick?.Invoke();
        }
    }
}
