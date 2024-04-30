using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Enemies.StateMachine
{
    public class State : MonoBehaviour
    {
        [SerializeField] private List<Transition> _transitions;
        [SerializeField] private Animator _animator;

        public Player.Player Target { get; private set; }
        public Animator Animator => _animator;

        public void Enter(Player.Player target)
        {
            if (enabled == false)
            {
                Target = target;
                enabled = true;

                foreach (var transition in _transitions)
                {
                    transition.enabled = true;
                    transition.Initiate(Target);
                }
            }
        }

        public void Exit()
        {
            foreach (var transition in _transitions) transition.enabled = false;

            enabled = false;
        }

        public State GetNextState()
        {
            foreach (var transition in _transitions)
                if (transition.NeedTransit)
                    return transition.TargetState;

            return null;
        }
    }
}