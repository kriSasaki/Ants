using UnityEngine;

namespace Source.Scripts.Enemies.StateMachine.Transitions
{
    public class DeathTransition : Transition
    {
        private const int Zero = 0;

        [SerializeField] private Enemy _enemy;

        private void Update()
        {
            if (_enemy.Health <= Zero) NeedTransit = true;
        }
    }
}