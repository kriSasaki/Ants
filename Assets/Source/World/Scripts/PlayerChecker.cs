using System;
using Source.Player.Scripts;
using UnityEngine;

namespace Source.World.Scripts
{
    public class PlayerChecker : MonoBehaviour
    {
        [SerializeField] private ResourceChecker _resourceChecker;
        
        public event Action<int> OnResearchMushroomNeeded;
        public event Action<int> OnResearchEggsNeeded;
        public event Action ConditionIsDone;

        private IDetectableObject _detectableObject;

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

                if (_resourceChecker.ResearchRecources(inventory.MushroomsCount, inventory.EggsCount))
                {
                    inventory.DeleteResources(_resourceChecker.Mushrooms, _resourceChecker.Eggs);
                    Destroy(gameObject);

                    ConditionIsDone?.Invoke();
                }
            }
        }
    }
}
