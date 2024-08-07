using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerFSM fsm, PlayerCharacter player) : base(fsm, player)
    {
    }

    public override void Update()
    {
        if (Time.time > _fsm.lastAttackTime + _player.Status.AttackInterval)
        {
            if (_player.AttackStrategy.SearchTarget())
            {
                _fsm.lastAttackTime = Time.time;
                _fsm.ChangeState(_fsm.AttackState);
            }
        }
    }
}