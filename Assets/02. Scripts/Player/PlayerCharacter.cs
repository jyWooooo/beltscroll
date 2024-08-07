using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private bool _isInitialized = false;

    public PlayerStatus Status { get; private set; }
    public PlayerFSM FSM { get; private set; }
    public IAttackStrategy AttackStrategy { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Initailize();
    }

    public void Initailize()
    {
        if (_isInitialized) return;

        FSM = new PlayerFSM(this);
        Animator = GetComponentInChildren<Animator>();

        _isInitialized = true;
    }

    public void ChangeCharacter(DB_PlayerCharacter data)
    {
        Animator.runtimeAnimatorController = data.Controller;
        AttackStrategy = data.CreateAttackStrategyInstance(this);
        Status = data.PlayerStatus;
    }

    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        AttackStrategy = strategy;
    }

    private void Update()
    {
        FSM.Update();
    }
}