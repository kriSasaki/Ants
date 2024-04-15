using UnityEngine;

namespace Source.Scripts.UI
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float _rotationX;
        [SerializeField] private float _rotationY;
        [SerializeField] private float _rotationZ;

        private void Update()
        {
            transform.Rotate(_rotationX * Time.unscaledDeltaTime, 
                _rotationY * Time.unscaledDeltaTime, 
                _rotationZ * Time.unscaledDeltaTime);
        }
    }
}
