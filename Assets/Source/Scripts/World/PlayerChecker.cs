using System;
using Source.Scripts.Player;
using UnityEngine;

namespace Source.Scripts.World
{
    public class PlayerChecker : MonoBehaviour
    {
        [SerializeField] private ResourceChecker _resourceChecker;

        private IDetectableObject _detectableObject;

        public event Action<int> OnResearchMushroomNeeded;
        public event Action<int> OnResearchEggsNeeded;
        public event Action ConditionIsDone;

        private void Awake()
        {
            _detectableObject = GetComponent<IDetectableObject>();
        }

        private void OnEnable()
        {
            _detectableObject.GameObjectDetected += OnGameObjectDetect;
        }

        private void OnDisable()
        {
            _detectableObject.GameObjectDetected -= OnGameObjectDetect;
        }

        private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
        {
            if (source.TryGetComponent(out Inventory inventory))
            {
                OnResearchMushroomNeeded?.Invoke(inventory.MushroomsCount);
                OnResearchEggsNeeded?.Invoke(inventory.EggsCount);

                if (_resourceChecker.ResearchResources(inventory.MushroomsCount, inventory.EggsCount))
                {
                    inventory.DeleteResources(_resourceChecker.Mushrooms, _resourceChecker.Eggs);
                    Destroy(gameObject);

                    ConditionIsDone?.Invoke();
                }
            }
        }
    }
}