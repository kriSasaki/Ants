using UnityEngine;

namespace Source.UI.Scripts
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
