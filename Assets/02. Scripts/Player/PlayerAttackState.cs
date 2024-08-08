using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private bool _isFired;

    public PlayerAttackState(PlayerFSM fsm, PlayerCharacter player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _isFired = false;
        _player.Animator.SetBool(_fsm.AnimatorAttackParameter, true);
    }

    public override void Update()
    {
        if (!_isFired && GetAnimStateTime() > 0.67f)
        {
            _player.AttackStrategy.Attack();
            _isFired = true;
        }
        if (GetAnimStateTime() >= 1f)
            _fsm.ChangeState(_fsm.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        _player.Animator.SetBool(_fsm.AnimatorAttackParameter, false);
    }
}