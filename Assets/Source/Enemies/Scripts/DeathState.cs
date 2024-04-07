using Source.Enemies.Scripts.StateMachine;
using UnityEngine;

namespace Source.Enemies.Scripts
{
    public class DeathState : State
    {
        private string _deathType;
        private BoxCollider _collider;
        private CapsuleCollider _capsule;

        private void OnEnable()
        {
            _collider = GetComponent<BoxCollider>();
            _capsule = GetComponent<CapsuleCollider>();
            PlayDeath();
        }

        private void PlayDeath()
        {
            _collider.enabled = false;
            _capsule.enabled = false;
            _deathType = ((DeathType)Random.Range((int)DeathType.AntDeath0, (int)DeathType.AntDeath02 + 1)).ToString();
            Animator.SetTrigger(Animator.StringToHash(_deathType));
        }

        private enum DeathType
        {
            AntDeath0,
            AntDeath01,
            AntDeath02
        }
    }
}
