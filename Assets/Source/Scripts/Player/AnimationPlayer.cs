using UnityEngine;

namespace Source.Scripts.Player
{
    public class AnimationPlayer : MonoBehaviour
    {
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _attack = Animator.StringToHash("Attack");
        private static readonly int _swordAttack = Animator.StringToHash("SwordAttack");
        private static readonly int _getHit = Animator.StringToHash("GetHit");

        [SerializeField] private Animator _animator;

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(_speed, speed);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(_attack);
        }

        public void PlaySwordAttack()
        {
            _animator.SetTrigger(_swordAttack);
        }

        public void PlayGetHit()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).GetHashCode() != _getHit) _animator.SetTrigger(_getHit);
        }
    }
}