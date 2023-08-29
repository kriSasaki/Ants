using UnityEngine;

public class NeededResourcesInstatiator : MonoBehaviour
{
    [SerializeField] private ResourceChecker _resourceChecker;
    [SerializeField] private ResourceIcon[] _neededResources;

    private ResourceIcon _resourceIcon;
    private readonly int _zero = 0;
    private readonly int _mushroomIcon = 0;

    private void Start()
    {
        if (_resourceChecker.Mushrooms > _zero)
        {
            _resourceIcon = Instantiate(_neededResources[_mushroomIcon], transform);
            _resourceIcon.SetNeededAmount(_resourceChecker.Mushrooms);
        }
    }
}
