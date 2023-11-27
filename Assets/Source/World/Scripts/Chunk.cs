using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private PlayerChecker _playerChecker;

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
        foreach(PieceOfChunk piece in _pieces)
        {
            piece.GetUp();
        }
    }
}
