using DG.Tweening;
using UnityEngine;

namespace Source.World.Scripts
{
    public class PieceOfChunk : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private float _positionY;

        private void Awake()
        {
            _positionY = transform.position.y;
            transform.position = new Vector3(transform.position.x, -20, transform.position.z);
        }

        public void GetUp()
        {
            transform.DOMoveY(_positionY, _duration).SetEase(Ease.OutBack);
        }
    }
}
