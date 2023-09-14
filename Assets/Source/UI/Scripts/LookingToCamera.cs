using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingToCamera : MonoBehaviour
{
    void Update()
    {
        if(transform.rotation != Camera.main.transform.rotation)
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}
