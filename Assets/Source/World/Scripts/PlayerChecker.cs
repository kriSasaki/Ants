using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private Chunk _chunk;

    private bool _isSpawned = false;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Player player) && _isSpawned==false)
        {
            _chunk.GetUp();
            _isSpawned = true;
        }
    }
}
