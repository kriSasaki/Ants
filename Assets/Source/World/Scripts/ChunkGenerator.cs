using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] private Chunk[] _startChunks;
    [SerializeField] private Chunk[] _chunks;

    private void Start()
    {
        Instantiate(_startChunks[Random.Range(0, _chunks.Length-1)]);
    }

    public void SpawnChank(Chunk chunk)
    {
        Chunk newChunk = Instantiate(_chunks[Random.Range(0, _chunks.Length)]);

        newChunk.transform.position = chunk.SpawnPoint.position - newChunk.SpawnPoint.localPosition;
    }
}
