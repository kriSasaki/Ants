using UnityEngine;

namespace Source.Scripts.Player
{
    public class AnimationPlayer : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int SwordAttack = Animator.StringToHash("SwordAttack");
        private static readonly int GetHit = Animator.StringToHash("GetHit");

        [SerializeField] private Animator _animator;

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void PlaySwordAttack()
        {
            _animator.SetTrigger(SwordAttack);
        }

        public void PlayGetHit()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).GetHashCode() != GetHit)
            {
                _animator.SetTrigger(GetHit);
            }
        }
    }
}
