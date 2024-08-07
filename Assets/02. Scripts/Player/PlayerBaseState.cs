public class PlayerBaseState : IState
{
    protected PlayerFSM _fsm;
    protected PlayerCharacter _player;

    public PlayerBaseState(PlayerFSM fsm, PlayerCharacter player)
    {
        _fsm = fsm;
        _player = player;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FixedUpdate() { }
    public virtual void HandleInput() { }
    public virtual void Update() { }
}