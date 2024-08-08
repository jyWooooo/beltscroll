using UnityEngine;

public class MonsterWalkState : MonsterBaseState
{
    public MonsterWalkState(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _monster.OnHit += OnHit;
    }

    public override void FixedUpdate()
    {
        _monster.Rigidbody.MovePosition(_monster.transform.position + _monster.Status.Speed * Time.fixedDeltaTime * Vector3.left);
    }

    public override void Exit()
    {
        base.Exit();
        _monster.OnHit -= OnHit;
    }

    public void OnHit()
    {
        _fsm.ChangeState(_fsm.HitState);
    }
}