using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    private bool ResourceCollected = false;

    protected Enemy _parent;
    protected int _amount = 1;
    protected DropAnimation _dropAnimation;
    protected PickUpAnimation _pickUpAnimation;

    void Start()
    {
        _pickUpAnimation = GetComponent<PickUpAnimation>();
        _dropAnimation = GetComponent<DropAnimation>();
    }

    protected abstract void NoticeResource(Inventory inventory, int amount);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy) && ResourceCollected == false)
        {
            ResourceCollected = true;
            _parent = enemy.GetComponent<Enemy>();
            DropResource();
        }
    }

    private void DropResource()
    {
        _pickUpAnimation.SetPosition3(_parent.Target.transform);
        NoticeResource(_parent.Target.GetComponent<Inventory>(), _amount);
        _dropAnimation.enabled = true;
    }
}
