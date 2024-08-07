using UnityEngine;

public class PlayerAttackableState : PlayerBaseState
{
    public PlayerAttackableState(PlayerFSM fsm, PlayerCharacter player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        _player.AttackStrategy.OnAttacked += PlayAttackAnimation;
    }

    public override void Exit()
    {
        _player.AttackStrategy.OnAttacked -= PlayAttackAnimation;
    }

    public override void Update()
    {
        if (Time.time > _fsm.lastAttackTime + _player.Status.AttackInterval)
        {
            if (_player.AttackStrategy.TryAttack())
                _fsm.lastAttackTime = Time.time;
        }
    }

    private void PlayAttackAnimation()
    {
        _player.Animator.SetTrigger(_fsm.AnimatorAttackTriggerParameter);
    }
}