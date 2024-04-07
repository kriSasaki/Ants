using Source.UI.Scripts;
using UnityEngine;

namespace Source.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private InterfacePresenter interfacePresenter;
        [SerializeField] private CameraFollower _cameraFollower;

        private void OnEnable()
        {
            interfacePresenter.StartButtonPressed += Enable;
        }

        private void OnDisable()
        {
            interfacePresenter.StartButtonPressed -= Enable;
        }
    
        private void Enable()
        {
            _cameraFollower.enabled = true;
        }
    }
}
