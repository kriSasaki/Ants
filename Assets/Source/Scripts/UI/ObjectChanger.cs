using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class ObjectChanger : MonoBehaviour
    {
        private const int FirstElement = 0;

        [SerializeField] private ScriptableObject[] _scriptableObjects;
        [SerializeField] private Player.Player _player;
        [SerializeField] private StartButton _startButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _leftButton;
        [SerializeField] private GameObject _rightButton;

        public ScriptableObject[] ScriptableObjects => _scriptableObjects;
        public Player.Player Player => _player;
        public StartButton StartButton => _startButton;
        public Button BuyButton => _buyButton;
        public int CurrentIndex { get; private set; }

        public virtual void ChangeScriptableObject(int change)
        {
            CurrentIndex += change;

            if (CurrentIndex < 0)
            {
                CurrentIndex = _scriptableObjects.Length - 1;
            }
            else if (CurrentIndex > _scriptableObjects.Length - 1)
            {
                CurrentIndex = 0;
            }

            _leftButton.SetActive(CurrentIndex != FirstElement);
            _rightButton.SetActive(CurrentIndex != _scriptableObjects.Length - 1);
        }
    }
}