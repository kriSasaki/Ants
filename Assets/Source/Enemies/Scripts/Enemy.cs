using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string GetDamage = "GetDamage";

    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private Egg _egg;
    [SerializeField] private int _eggsAmount;
    [SerializeField] private Leg _leg;
    [SerializeField] private int _legsAmount;

    public Player Target => _target;

    public event UnityAction<Enemy> Dying;

    private AudioSource _audio;

    private Animator _animator;
    private int _zero=0;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger(GetDamage);
        _audio.Play();

        if (_health <= 0)
        {
            Dying?.Invoke(this);
            DropResources();
            Destroy(gameObject);
        }
    }

    private void DropResources()
    {
        if(_eggsAmount > _zero)
        {
            for(int i = 0; i <= _eggsAmount; i++)
            {
                Instantiate(_egg);
            }
        }

        if(_legsAmount > _zero)
        {
            for (int i = 0; i <= _legsAmount; i++)
            {
                Instantiate(_leg);
            }
        }
    }
}
