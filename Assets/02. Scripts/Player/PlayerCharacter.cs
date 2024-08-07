using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public PlayerStatus Status { get; private set; }
    public PlayerFSM FSM { get; private set; }
    public IAttackStrategy AttackStrategy { get; private set; }
    public Animator Animator { get; private set; }
    public Collider2D AttackTrigger { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        AttackTrigger = transform.GetChild(1).GetComponent<Collider2D>();
    }

    public void ChangeCharacter(DB_PlayerCharacter data)
    {
        Animator.runtimeAnimatorController = data.Controller;
        AttackStrategy = data.CreateAttackStrategyInstance(this);
        Status = data.PlayerStatus;
        FSM ??= new(this);
    }

    private void Update()
    {
        FSM.Update();
    }
}