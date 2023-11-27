using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public static event Action PlayerEnter;
    public event Action ConditionIsDone;

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
        if (source.TryGetComponent(out Inventory inventory))
        {
            PlayerEnter?.Invoke();

            if (_resourceChecker.MushroomCollected && _resourceChecker.EggsCollected && _resourceChecker.LegsCollected)
            {
                inventory.DeleteResources(_resourceChecker.Mushrooms, _resourceChecker.Eggs, _resourceChecker.Legs);
                Destroy(gameObject);

                ConditionIsDone?.Invoke();
            }
        }
    }
}
