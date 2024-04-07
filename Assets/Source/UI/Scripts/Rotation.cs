using UnityEngine;

namespace Source.UI.Scripts
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private float _rotarionX;
        [SerializeField] private float _rotarionY;
        [SerializeField] private float _rotarionZ;


        void Update()
        {
            transform.Rotate(_rotarionX * Time.unscaledDeltaTime, _rotarionY * Time.unscaledDeltaTime, _rotarionZ * Time.unscaledDeltaTime);
        }
    }
}
