using UnityEngine;

namespace Source.Scripts.UI
{
    public class LookingToCamera : MonoBehaviour
    {
        void Update()
        {
            if(transform.rotation != UnityEngine.Camera.main.transform.rotation)
            {
                transform.rotation = UnityEngine.Camera.main.transform.rotation;
            }
        }
    }
}
