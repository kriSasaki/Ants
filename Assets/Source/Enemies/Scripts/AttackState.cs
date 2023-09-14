using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private float _lastAttackTime;
    private string _attackType;

    private void Update()
    {
        if (_lastAttackTime <= 0)
        {
            Attack(Target);

            _lastAttackTime = _delay;
        }

        _lastAttackTime -= Time.deltaTime;
    }

    private void OnEnable()
    {
        _lastAttackTime = 1;
    }

    private void Attack(Player target)
    {
        _attackType = ((AttackType)Random.Range((int)AttackType.AntAttack0, (int)AttackType.AntAttack02 + 1)).ToString();
        Animator.SetTrigger(_attackType);
        target.GetDamage(_damage);
    }

    private enum AttackType
    {
        AntAttack0,
        AntAttack01,
        AntAttack02
    }
}
