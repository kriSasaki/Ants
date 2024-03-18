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
