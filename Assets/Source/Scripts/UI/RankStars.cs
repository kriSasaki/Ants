using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class RankStars : MonoBehaviour
    {
        [SerializeField] private List<ActivitySwitcher> _rankStars;

        public void ShowStars(int amount)
        {
            for (var i = 0; i < _rankStars.Count; i++)
            {
                if (i < amount)
                {
                    _rankStars[i].Show();
                }
                else
                {
                    _rankStars[i].Hide();
                }
            }
        }
    }
}