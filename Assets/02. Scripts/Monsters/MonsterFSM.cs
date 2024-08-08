using UnityEngine;

public class MonsterFSM : FSM
{
    public int AnimatorHitParameter { get; private set; } = Animator.StringToHash("Hit");
    public int AnimatorDieParameter { get; private set; } = Animator.StringToHash("Die");

    public MonsterWalkState WalkState { get; private set; }
    public MonsterHitState HitState { get; private set; }
    public MonsterDeadState DeadState { get; private set; }

    public MonsterFSM(MonsterBase monster)
    {
        WalkState = new(monster, this);
        HitState = new(monster, this);
        DeadState = new(monster, this);
        _currentState = WalkState;
        WalkState.Enter();
    }
}