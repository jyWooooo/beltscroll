public class MonsterBaseState : IState
{
    protected MonsterBase _monster;
    protected MonsterFSM _fsm;

    public MonsterBaseState(MonsterBase monster, MonsterFSM fsm)
    {
        _monster = monster;
        _fsm = fsm;
    }

    public virtual void Enter() 
    {
        _monster.OnDied += OnDead;
    }

    public virtual void Exit() 
    {
        _monster.OnDied -= OnDead;
    }

    public virtual void FixedUpdate() { }
    public virtual void HandleInput() { }
    public virtual void Update() { }

    public float GetAnimStateTime()
    {
        return _monster.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void OnDead()
    {
        _fsm.ChangeState(_fsm.DeadState);
    }
}