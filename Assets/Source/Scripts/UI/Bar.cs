using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public abstract class Bar : MonoBehaviour
    {
        private const float DefaultSliderValue = 1;
        
        [SerializeField] private Slider Slider;
     
        public void ValueChanged(int value, int maxValue)
        {
            Slider.value = (float)value / maxValue;
        }

        public void ResetToDefault()
        {
            Slider.value = DefaultSliderValue;
        }
    }
}
