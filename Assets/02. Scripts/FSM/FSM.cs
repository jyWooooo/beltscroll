public abstract class FSM
{
    protected IState _currentState;

    public void ChangeState(IState nextState)
    {
        if (nextState == _currentState)
            return;

        _currentState?.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    public virtual void Update()
    {
        _currentState.Update();
    }

    public virtual void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public virtual void HandleInput()
    {
        _currentState.HandleInput();
    }
}