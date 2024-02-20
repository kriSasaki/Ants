using UnityEngine;
using UnityEngine.Serialization;

public abstract class Resource : MonoBehaviour
{
    [SerializeReference] protected float _pickUpDuration;
    
    private bool ResourceCollected = false;

    protected Player _target;
    protected int _amount = 1;
    protected DropAnimation _dropAnimation;
    protected PickUpAnimation _pickUpAnimation;
    protected Coroutine _coroutine;
    protected float _time;
    
    void Start()
    {
        _pickUpAnimation = GetComponent<PickUpAnimation>();
        _dropAnimation = GetComponent<DropAnimation>();
    }

    protected abstract void NoticeResource(Inventory inventory, int amount);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player target) && ResourceCollected == false)
        {
            ResourceCollected = true;
            _target = target;
            DropResource();
        }
    }

    private void DropResource()
    {
        _pickUpAnimation.SetPosition3(_target.transform);
        NoticeResource(_target.GetComponent<Inventory>(), _amount);
        _dropAnimation.enabled = true;
    }
}
