using Source.Enemies.Scripts.StateMachine;
using UnityEngine;

namespace Source.Enemies.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class AttackState : State
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _delay;
        [SerializeField] private float _radianAngle;

        private float _lastAttackTime;
        private string _attackType;

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

        private void Attack(Player.Scripts.Player target)
        {
            _attackType = ((AttackType)Random.Range((int)AttackType.AntAttack0, (int)AttackType.AntAttack02 + 1)).ToString();
            Animator.SetTrigger(Animator.StringToHash(_attackType));
            target.TakeDamage(_damage);
        }

        private void LookToTarget()
        {
            Vector3 newDir = Vector3.RotateTowards(transform.forward, (Target.transform.position - transform.position), _radianAngle, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        private enum AttackType
        {
            AntAttack0,
            AntAttack01,
            AntAttack02
        }
    }
}
