using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Scripts
{
    public class ObjectChanger : MonoBehaviour
    {
        private const int _firstElement = 0;
    
        [SerializeField] private ScriptableObject[] _scriptableObjects;
        [SerializeField] private Player.Scripts.Player _player;
        [SerializeField] private InterfacePresenter _interfacePresenter;
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _leftButton;
        [SerializeField] private GameObject _rightButton;

        public ScriptableObject[] ScriptableObjects => _scriptableObjects;
        public Player.Scripts.Player Player => _player;
        public InterfacePresenter InterfacePresenter => _interfacePresenter;
        public Button BuyButton => _buyButton;
        public int CurrentIndex => _currentIndex;
    
        private int _currentIndex = 0;
    
        public virtual void ChangeScriptableObject(int change)
        {
            _currentIndex += change;

            if (_currentIndex < 0)
            {
                _currentIndex = _scriptableObjects.Length - 1;
            }
            else if (_currentIndex > _scriptableObjects.Length - 1)
            {
                _currentIndex = 0;
            }
        
            _leftButton.SetActive(_currentIndex != _firstElement);
            _rightButton.SetActive(_currentIndex != _scriptableObjects.Length-1);
        }
    }
}
