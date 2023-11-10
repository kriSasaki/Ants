using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    private bool ResourceCollected = false;

    protected Enemy _parent;

    protected DropAnimation _dropAnimation;
    protected PickUpAnimation _pickUpAnimation;

    void Start()
    {
        _pickUpAnimation = GetComponent<PickUpAnimation>();
        _dropAnimation = GetComponent<DropAnimation>();
    }

    protected abstract void NoticeResource();

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
        NoticeResource();
        _pickUpAnimation.SetPosition3(_parent.Target.transform);
        _dropAnimation.enabled = true;
    }
}
