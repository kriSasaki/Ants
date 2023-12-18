using System;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public event Action<int> OnResearchMushroomNeeded;
    public event Action<int> OnResearchEggsNeeded;
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
            OnResearchMushroomNeeded?.Invoke(inventory.MushroomsCount);
            OnResearchEggsNeeded?.Invoke(inventory.EggsCount);

            if (_resourceChecker.ResearchRecources(inventory.MushroomsCount, inventory.EggsCount))
            {
                inventory.DeleteResources(_resourceChecker.Mushrooms, _resourceChecker.Eggs, _resourceChecker.Legs);
                Destroy(gameObject);

                ConditionIsDone?.Invoke();
            }
        }
    }
}
