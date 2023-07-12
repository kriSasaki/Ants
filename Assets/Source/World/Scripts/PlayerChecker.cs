using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private ChunkGenerator _chunkGenerator;
    [SerializeField] private Chunk _chunk;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player))
        {
            _chunkGenerator.SpawnChank(_chunk);
        }
    }
}
