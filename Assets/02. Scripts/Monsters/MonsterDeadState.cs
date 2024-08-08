using UnityEngine;

public class MonsterDeadState : MonsterBaseState
{
    private StageManager _stageManager;
    private Stage _stage;

    public MonsterDeadState(MonsterBase monster, MonsterFSM fsm) : base(monster, fsm)
    {
        _stageManager = GameManager.StageManager;
        _stage = _monster.GetComponent<Stage>();
    }

    public override void Enter()
    {
        _monster.Animator.SetBool(_fsm.AnimatorDieParameter, true);
    }

    public override void Update()
    {
        if (GetAnimStateTime() > 1f)
        {
            _stageManager.RemoveStage(_stage);
            Object.Destroy(_monster.gameObject);
        }
    }
}