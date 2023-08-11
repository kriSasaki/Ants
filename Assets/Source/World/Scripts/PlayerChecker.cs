using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private Chunk[] _chunks;

    private bool _isSpawned = false;
    private ResourceChecker _resourceChecker;
    private IDetectableObject _detectableObject;

    private void Awake()
    {
        _resourceChecker = GetComponent<ResourceChecker>();
        _detectableObject = GetComponent<IDetectableObject>();
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
    }

    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        if(source.TryGetComponent(out Player player))
        {
            player.GiveAwayResources();

            if (_resourceChecker.CheckResources(player))
            {
                Destroy(gameObject);

                foreach (Chunk chunk in _chunks)
                {
                    chunk.GetUp();
                }
            }
        }
    }
}
