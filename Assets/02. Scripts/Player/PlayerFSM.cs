using UnityEngine;

public class PlayerFSM : FSM
{
    protected PlayerCharacter _player;
    public float lastAttackTime;

    public int AnimatorAttackParameter { get; private set; } = Animator.StringToHash("Attack");
    public int AnimatorWalkParameter { get; private set; } = Animator.StringToHash("Walk");

    public PlayerIdleState IdleState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }

    public PlayerFSM(PlayerCharacter player)
    {
        _player = player;
        IdleState = new(this, player);
        AttackState = new(this, player);
        _currentState = IdleState;
        _currentState.Enter();

    }
}