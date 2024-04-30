using UnityEngine;

namespace Source.Scripts.World
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(PieceOfChunk[]))]
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private PlayerChecker _playerChecker;
        [SerializeField] private AudioSource _audioSource;

        private PieceOfChunk[] _pieces;

        private void OnEnable()
        {
            _playerChecker.ConditionIsDone += GetUp;
        }

        private void OnDisable()
        {
            _playerChecker.ConditionIsDone -= GetUp;
        }

        private void Start()
        {
            _pieces = GetComponentsInChildren<PieceOfChunk>();
        }

        private void GetUp()
        {
            _audioSource.Play();

            foreach (var piece in _pieces) piece.GetUp();
        }
    }
}