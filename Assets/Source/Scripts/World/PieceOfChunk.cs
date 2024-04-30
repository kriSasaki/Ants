using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.World
{
    public class PieceOfChunk : MonoBehaviour
    {
        private const int HidedYPosition = -20;

        [SerializeField] private float _duration;

        private float _positionY;

        private void Awake()
        {
            _positionY = transform.position.y;
            transform.position = new Vector3(transform.position.x, HidedYPosition, transform.position.z);
        }

        public void GetUp()
        {
            transform.DOMoveY(_positionY, _duration).SetEase(Ease.OutBack);
        }
    }
}