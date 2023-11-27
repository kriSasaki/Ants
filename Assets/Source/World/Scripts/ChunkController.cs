using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Chunk[] _chunks;

    private PlayerChecker _playerChecker;

    private void Start()
    {
        _playerChecker = GetComponent<PlayerChecker>();
    }

    private void OnEnable()
    {
        _playerChecker.ConditionIsDone += ShowChunks;
    }

    private void OnDisable()
    {
        _playerChecker.ConditionIsDone -= ShowChunks;
    }

    private void ShowChunks()
    {
        foreach (Chunk chunk in _chunks)
        {
            //chunk.GetUp();
        }
    }
}
