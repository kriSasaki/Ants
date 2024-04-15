using UnityEngine;

namespace Source.Scripts.Enemies.StateMachine
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private State _targetState;

        public Player.Player Target { get; private set; }

        public State TargetState => _targetState;

        public bool NeedTransit { get; protected set; }

        public void Initiate(Player.Player target)
        {
            Target = target;
        }

        private void OnEnable()
        {
            NeedTransit = false;
        }
    }
}
