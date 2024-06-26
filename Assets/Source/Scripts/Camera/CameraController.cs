using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private StartButton _startButton;
        [SerializeField] private TransformFollower _transformFollower;

        private void OnEnable()
        {
            _startButton.OnClick += Enable;
        }

        private void OnDisable()
        {
            _startButton.OnClick -= Enable;
        }

        private void Enable()
        {
            _transformFollower.enabled = true;
        }
    }
}