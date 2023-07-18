using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkStorage : MonoBehaviour
{
    [SerializeField] private Chunk[] _startChunks;
    [SerializeField] private Chunk[] _transitionalChunks;
    [SerializeField] private Chunk[] _chunks;

    private void Start()
    {
        Debug.Log(_transitionalChunks.Length);
    }

    public Chunk GetRandomStartChunk()
    {
        return _startChunks[Random.Range(0, _startChunks.Length - 1)];
    }

    public Chunk GetRandomTransitionalChunk()
    {
        return _transitionalChunks[Random.Range(0, _transitionalChunks.Length)];
    }

    public Chunk GetRandomChunk()
    {
        return _chunks[Random.Range(0, _chunks.Length - 1)];
    }
}
