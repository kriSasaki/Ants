using System;
using UnityEngine;

namespace Source.Player.Scripts
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
    
        public event Action<int> MushroomsAmountChanged;
        public event Action<int> EggsAmountChanged;

        public int MushroomsCount {  get; private set; }
        public int EggsCount { get; private set; }

        public void DeleteResources(int mushrooms, int eggs) 
        {
            ChangeMushroomsAmount(-mushrooms);
            ChangeEggsAmount(-eggs);
        }

        public void ChangeMushroomsAmount(int amount)
        {
            MushroomsCount += amount;
            MushroomsAmountChanged?.Invoke(MushroomsCount);
            _audioSource.Play();
        }

        public void ChangeEggsAmount(int amount)
        {
            EggsCount += amount;
            EggsAmountChanged?.Invoke(EggsCount);
            _audioSource.Play();
        }
    }
}
