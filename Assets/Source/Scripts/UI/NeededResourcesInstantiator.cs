using Source.Scripts.World;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class NeededResourcesInstantiator : MonoBehaviour
    {
        private const int Zero = 0;
        private const int MushroomIcon = 0;
        private const int EggIcon = 1;

        [SerializeField] private PlayerChecker _playerChecker;
        [SerializeField] private ResourceChecker _resourceChecker;
        [SerializeField] private ResourceIcon[] _neededResources;

        private ResourceIcon _resourceIcon;

        private void Start()
        {
            if (_resourceChecker.Mushrooms > Zero)
            {
                _resourceIcon = Instantiate(_neededResources[MushroomIcon], transform);
                _resourceIcon.Initialize(_playerChecker, _resourceChecker.Mushrooms);
            }

            if (_resourceChecker.Eggs > Zero)
            {
                _resourceIcon = Instantiate(_neededResources[EggIcon], transform);
                _resourceIcon.Initialize(_playerChecker, _resourceChecker.Eggs);
            }
        }
    }
}