using System.Collections;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private const int Amount = 1;

    [SerializeField] private float _jumpDuration;

    private SphereCollider _sphereCollider;
    private IDetectableObject _detectableObject;
    private PickUpAnimation _pickUpAnimation;
    private Coroutine _coroutine;
    private float _time;

    private void Awake()
    {
        _pickUpAnimation = GetComponent<PickUpAnimation>();
        _detectableObject = GetComponent<IDetectableObject>();
    }

    private void Start()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        _detectableObject.OnGameObjectDetectEvent += OnGameObjectDetect;
    }

    private void OnDisable()
    {
        _detectableObject.OnGameObjectDetectEvent -= OnGameObjectDetect;
    }

    private void JumpIn(Transform playerPosition)
    {
        _sphereCollider.enabled = false;
        _pickUpAnimation.SetPosition3(playerPosition);
        _pickUpAnimation.enabled = true;
        Destroy(gameObject, _jumpDuration);
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        if (source.TryGetComponent(out Inventory inventory))
        {
            JumpIn(inventory.transform);
            _coroutine = StartCoroutine(GiveResource(inventory));
        }
    }

    private IEnumerator GiveResource(Inventory inventory)
    {
        _time = 0.5f;
        
        while (_time < _jumpDuration)
        {
            _time += Time.deltaTime;
            yield return null;
        }

        inventory.ChangeMushroomsAmount(Amount);
    }
}