using UnityEngine;

public class PlayerFSM : FSM
{
    protected PlayerCharacter _player;
    public float lastAttackTime;

    public int AnimatorAttackTriggerParameter { get; private set; } = Animator.StringToHash("Attack");
    public int AnimatorWalkBoolParameter { get; private set; } = Animator.StringToHash("Walk");

    public PlayerAttackableState AttackableState { get; private set; }

    public PlayerFSM(PlayerCharacter player)
    {
        _player = player;
        AttackableState = new(this, player);
        _currentState = AttackableState;
    }
}