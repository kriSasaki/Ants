using UnityEngine;

namespace Source.World.Scripts
{
    public class TimeScaleChanger : MonoBehaviour
    {
        public void Stop()
        {
            Time.timeScale = 0;
        }

        public void Start()
        {
            Time.timeScale = 1;
        }
    }
}
