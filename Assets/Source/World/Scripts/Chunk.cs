using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private PieceOfChunk[] _pieces;

    private void Start()
    {
        _pieces = GetComponentsInChildren<PieceOfChunk>();
    }

    public void GetUp()
    {
        foreach(PieceOfChunk piece in _pieces)
        {
            piece.GetUp();
        }
    }
}
