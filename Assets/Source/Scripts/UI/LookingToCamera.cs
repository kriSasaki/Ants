using UnityEngine;

namespace Source.Scripts.UI
{
    public class LookingToCamera : MonoBehaviour
    {
        private void Update()
        {
            if (transform.rotation != UnityEngine.Camera.main.transform.rotation)
                transform.rotation = UnityEngine.Camera.main.transform.rotation;
        }
    }
}