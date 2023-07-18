using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    private ChunkStorage _chunkStorage;

    private int _chunkMinCount = 4;
    private int _chunkMaxCount = 5;
    private int _chunkTotalCount;
    private int _chunkCount;

    private bool _isTransitionalSpawned = false;

    private void Awake()
    {
        _chunkTotalCount = Random.Range(_chunkMinCount, _chunkMaxCount);
        _chunkStorage = GetComponent<ChunkStorage>();
        Instantiate(_chunkStorage.GetRandomStartChunk());
    }

    public void SpawnChunk(Chunk chunk)
    {
        Debug.Log("1");
        Chunk newChunk = Instantiate(_chunkStorage.GetRandomTransitionalChunk());
        newChunk.transform.position = chunk.BeginPoint.position - newChunk.EndPoint.localPosition;
        _chunkCount++;
    }
}
