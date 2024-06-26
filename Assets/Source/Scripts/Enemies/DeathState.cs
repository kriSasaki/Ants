using Source.Scripts.Enemies.StateMachine;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    public class DeathState : State
    {
        [SerializeField] private BoxCollider _collider;
        [SerializeField] private CapsuleCollider _capsule;

        private string _deathType;

        private enum DeathType
        {
            AntDeath0,
            AntDeath01,
            AntDeath02,
        }

        private void OnEnable()
        {
            PlayDeath();
        }

        private void PlayDeath()
        {
            _collider.enabled = false;
            _capsule.enabled = false;
            _deathType = ((DeathType)Random.Range((int)DeathType.AntDeath0, (int)DeathType.AntDeath02 + 1)).ToString();
            Animator.SetTrigger(Animator.StringToHash(_deathType));
        }
    }
}