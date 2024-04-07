using UnityEngine;

namespace Source.Player.Scripts
{
    public class AnimationPlayer : MonoBehaviour
    {
        private static readonly int Speed1 = Animator.StringToHash("Speed");
        private static readonly int Attack1 = Animator.StringToHash("Attack");
        private static readonly int SwordAttack1 = Animator.StringToHash("SwordAttack");
        private static readonly int GetHit = Animator.StringToHash("GetHit");

        [SerializeField] private Animator _animator;

        public void SetSpeed(float speed)
        {
            _animator.SetFloat(Speed1, speed);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack1);
        }

        public void PlaySwordAttack()
        {
            _animator.SetTrigger(SwordAttack1);
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
