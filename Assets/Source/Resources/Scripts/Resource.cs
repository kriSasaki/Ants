using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    protected Enemy _parent;

    protected DropAnimation _dropAnimation;
    protected PickUpAnimation _pickUpAnimation;

    void Start()
    {
        _pickUpAnimation = GetComponent<PickUpAnimation>();
        _dropAnimation = GetComponent<DropAnimation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            _parent = enemy.GetComponent<Enemy>();
            DropResource();
        }
    }

    private void DropResource()
    {
        Debug.Log("lol");
        _pickUpAnimation.SetPosition3(_parent.Target.transform);
        _dropAnimation.enabled = true;
    }
}
