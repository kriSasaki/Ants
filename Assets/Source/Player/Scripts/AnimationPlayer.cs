using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string Attack = "Attack";
    private const string SwordAttack = "SwordAttack";
    private const string GetHit = "GetHit";

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

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
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(GetHit) == false)
        {
            _animator.SetTrigger(GetHit);
        }
    }
}
