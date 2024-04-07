using Source.World.Scripts;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class NeededResourcesInstatiator : MonoBehaviour
    {
        private const int _zero = 0;
        private const int _mushroomIcon = 0;
        private const int _eggIcon = 1;
        
        [SerializeField] private PlayerChecker _playerChecker;
        [SerializeField] private ResourceChecker _resourceChecker;
        [SerializeField] private ResourceIcon[] _neededResources;

        private ResourceIcon _resourceIcon;

        private void Start()
        {
            if (_resourceChecker.Mushrooms > _zero)
            {
                _resourceIcon = Instantiate(_neededResources[_mushroomIcon], transform);
                _resourceIcon.Initialize(_playerChecker, _resourceChecker.Mushrooms);
            }
            if(_resourceChecker.Eggs > _zero)
            {
                _resourceIcon = Instantiate(_neededResources[_eggIcon], transform);
                _resourceIcon.Initialize(_playerChecker, _resourceChecker.Eggs);
            }
        }
    }
}
