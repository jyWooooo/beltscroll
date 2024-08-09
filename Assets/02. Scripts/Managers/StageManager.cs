using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager
{
    private float _moveNextStageAnimateTime;
    private HashSet<Stage> _stages;
    private int _stageCnt;

    public event System.Action<int> OnNextStageMoveStarted;
    public event System.Action<int> OnNextStageMoveFinished;

    public void Initialize()
    {
        _moveNextStageAnimateTime = GameManager.DataManager.GetData<DB_WorldSettings>("WorldSettings").MoveNextStageAnimateTime;
    }

    public void Clear()
    {
        foreach (var d in OnNextStageMoveStarted.GetInvocationList())
            OnNextStageMoveStarted -= (System.Action<int>)d;

        foreach (var d in OnNextStageMoveFinished.GetInvocationList())
            OnNextStageMoveFinished -= (System.Action<int>)d;
    }

    public void CreateStage()
    {
        _stages = Object.Instantiate(GameManager.ResourceManager.GetCache<GameObject>("Map.prefab")).GetComponentsInChildren<Stage>().ToHashSet();
    }

    public void MoveNextStage()
    {
        _stageCnt++;
        OnNextStageMoveStarted?.Invoke(_stageCnt);
        GameManager.Instance.StartCoroutine(AnimateMoveNextStage());
    }

    public void AddStage(Stage stage)
    {
        _stages.Add(stage);
    }

    public void RemoveStage(Stage stage)
    {
        _stages.Remove(stage);
    }

    private IEnumerator AnimateMoveNextStage()
    {
        for (float f = 0; f < _moveNextStageAnimateTime; f += Time.deltaTime)
        {
            foreach (var stage in _stages) 
                stage.MoveNextStage();

            yield return null;
        }
        OnNextStageMoveFinished?.Invoke(_stageCnt);
    }
}