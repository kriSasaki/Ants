using UnityEngine;

namespace Source.Enemies.Scripts.StateMachine.Transitions
{
    public class DeathTransition : Transition
    {
        private const int _zero = 0;
    
        [SerializeField] private Enemy _enemy; 
    
        private void Update()
        {
            if (_enemy.Health <= _zero)
            {
                NeedTransit = true;
            }
        }
    }
}