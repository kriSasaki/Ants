using System.Collections.Generic;
using UnityEngine;

namespace Source.UI.Scripts
{
    public class RankStars : MonoBehaviour
    {
        [SerializeField] private List<RankStar> _rankStars;

        public void ShowStars(int amount)
        {
            for (int i = 0; i < _rankStars.Count; i++)
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
