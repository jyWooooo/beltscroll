using UnityEngine;

public class PlayerFSM : FSM
{
    public float lastAttackTime;

    public int AnimatorAttackParameter { get; private set; } = Animator.StringToHash("Attack");
    public int AnimatorWalkParameter { get; private set; } = Animator.StringToHash("Walk");

    public PlayerIdleState IdleState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerWalkState WalkState { get; private set; }

    public PlayerFSM(PlayerCharacter player)
    {
        IdleState = new(this, player);
        AttackState = new(this, player);
        WalkState = new(this, player);
        _currentState = IdleState;
        _currentState.Enter();
    }
}