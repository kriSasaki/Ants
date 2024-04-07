using UnityEngine;

namespace Source.Enemies.Scripts.StateMachine
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public Player.Scripts.Player Target { get; private set; }

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        public void Initiate(Player.Scripts.Player target)
        {
            Target = target;
        }

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}
