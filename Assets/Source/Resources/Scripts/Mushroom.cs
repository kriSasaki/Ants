using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _shakeVibration;
    [SerializeField] private float _shakeRandomness;

    private SphereCollider _sphereCollider;
    private IDetectableObject _detectableObject;
    private PickUpAnimation _pickUpAnimation;
    private int _amount = 1;

    private void Awake()
    {
        _pickUpAnimation= GetComponent<PickUpAnimation>();
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

    public void JumpIn(Vector3 playerPosition)
    {
        _sphereCollider.enabled = false;
        _pickUpAnimation.enabled = true;
        Destroy(gameObject, _jumpDuration);
    }

    private void OnGameObjectDetect(GameObject source, GameObject detectedObject)
    {
        if (source.TryGetComponent(out Inventory inventory))
        {
            inventory.ChangeMushroomsAmount(_amount);
            JumpIn(source.transform.position);
        }
    }
}
