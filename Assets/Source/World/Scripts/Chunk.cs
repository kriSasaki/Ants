using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Chunk : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

    private PieceOfChunk[] _pieces;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

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
        
        foreach(PieceOfChunk piece in _pieces)
        {
            piece.GetUp();
        }
    }
}
