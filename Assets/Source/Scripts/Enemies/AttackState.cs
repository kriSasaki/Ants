using Source.Scripts.Enemies.StateMachine;
using UnityEngine;

namespace Source.Scripts.Enemies
{
    [RequireComponent(typeof(Animator))]
    public class AttackState : State
    {
        private const float MaxMagnitudeDelta = 0;

        [SerializeField] private int _damage;
        [SerializeField] private float _delay;
        [SerializeField] private float _radianAngle;

        private float _lastAttackTime;
        private string _attackType;
        private Vector3 _direction;

        private enum AttackType
        {
            AntAttack0,
            AntAttack01,
            AntAttack02,
        }

        private void Update()
        {
            LookToTarget();

            if (_lastAttackTime <= 0)
            {
                Attack(Target);

                _lastAttackTime = _delay;
            }

            _lastAttackTime -= Time.deltaTime;
        }

        private void OnEnable()
        {
            _lastAttackTime = _delay;
        }

        private void Attack(Player.Player target)
        {
            _attackType = ((AttackType)Random.Range((int)AttackType.AntAttack0, (int)AttackType.AntAttack02 + 1))
                .ToString();
            Animator.SetTrigger(Animator.StringToHash(_attackType));
            target.TakeDamage(_damage);
        }

        private void LookToTarget()
        {
            _direction = Vector3.RotateTowards(
                transform.forward,
                Target.transform.position - transform.position,
                _radianAngle,
                MaxMagnitudeDelta);
            transform.rotation = Quaternion.LookRotation(_direction);
        }
    }
}