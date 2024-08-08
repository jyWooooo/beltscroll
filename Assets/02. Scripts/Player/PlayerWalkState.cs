using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerFSM fsm, PlayerCharacter player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.Animator.SetBool(_fsm.AnimatorWalkParameter, true);
    }

    public override void Exit()
    {
        base.Exit();
        _player.Animator.SetBool(_fsm.AnimatorWalkParameter, false);
    }
}