using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerFSM _fsm;
    protected PlayerCharacter _player;

    public PlayerBaseState(PlayerFSM fsm, PlayerCharacter player)
    {
        _fsm = fsm;
        _player = player;
    }

    public virtual void Enter() 
    {
        GameManager.StageManager.OnNextStageMoveStarted += OnNextStageMoveStart;
        GameManager.StageManager.OnNextStageMoveFinished += OnNextStageMoveFinish;
    }

    public virtual void Exit()
    {
        GameManager.StageManager.OnNextStageMoveStarted -= OnNextStageMoveStart;
        GameManager.StageManager.OnNextStageMoveFinished -= OnNextStageMoveFinish;
    }

    public virtual void FixedUpdate() { }
    public virtual void HandleInput() { }
    public virtual void Update() { }

    public float GetAnimStateTime()
    {
        return _player.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void OnNextStageMoveStart(int _)
    {
        _fsm.ChangeState(_fsm.WalkState);
    }

    public void OnNextStageMoveFinish(int _)
    {
        _fsm.ChangeState(_fsm.IdleState);
    }
}