using Source.Scripts.Enemies.StateMachine;

namespace Source.Scripts.Enemies
{
    public class IdleState : State
    {
        private void Update()
        {
            transform.position = transform.position;
        }
    }
}