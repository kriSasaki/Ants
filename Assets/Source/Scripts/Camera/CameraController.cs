using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InterfacePresenter _interfacePresenter;
        [SerializeField] private TransformFollower _transformFollower;

        private void OnEnable()
        {
            _interfacePresenter.StartButtonPressed += Enable;
        }

        private void OnDisable()
        {
            _interfacePresenter.StartButtonPressed -= Enable;
        }

        private void Enable()
        {
            _transformFollower.enabled = true;
        }
    }
}