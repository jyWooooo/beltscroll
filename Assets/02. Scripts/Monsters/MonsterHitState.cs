using System.Collections;
using UnityEngine;

public class MonsterHitState : MonsterBaseState
{
    public MonsterHitState(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _monster.Animator.SetBool(_fsm.AnimatorHitParameter, true);
    }

    public override void Update()
    {
        if (GetAnimStateTime() >= 1f)
            _fsm.ChangeState(_fsm.WalkState);
    }

    public override void FixedUpdate()
    {
        _monster.Rigidbody.MovePosition(_monster.transform.position + CalculateKnockbackForce(GetAnimStateTime()) * Time.fixedDeltaTime * Vector3.right);
    }

    public override void Exit()
    {
        base.Exit();
        _monster.Animator.SetBool(_fsm.AnimatorHitParameter, false);
    }

    public float CalculateKnockbackForce(float t)
    {
        return Mathf.Lerp(10f, 0f, t);
    }
}