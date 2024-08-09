using System;
using System.Collections;
using UnityEngine;

public class MainScene : SceneBase
{
    [SerializeField] private UI_Loading _uiLoading;

    public override void LoadScene()
    {
        ReadFirebaseDB(() =>
        {
            LoadResources(() =>
            {
                StartGame();
            });
        });
    }

    private void ReadFirebaseDB(Action callback)
    {
        _uiLoading.SetMessage("Checking Network Connection");
        GameManager.FirebaseDBManager.TryRead((result) =>
        {
            Debug.Log($"Firebase Database Read Success. Value : {result}");
            GameManager.StageManager.SetStageCount(result - 1);
            callback?.Invoke();
        }, () =>
        {
            Debug.Log("Firebase Database Read Fail");
            callback?.Invoke();
        });
    }

    private void LoadResources(Action callback)
    {
        _uiLoading.SetMessage("Loading Resources");
        GameManager.ResourceManager.LoadAllAsync<UnityEngine.Object>("MainScene", (key, cnt, total) =>
        {
            if (cnt == total)
            {
                Debug.Log("MainScene Resources Load Complete");
                callback?.Invoke();
            }
        });
    }

    private void InitializeStage()
    {
        GameManager.StageManager.CreateStage();
        GameManager.StageManager.OnNextStageMoveFinished += (stage) => GameManager.MonsterManager.SpawnMonster(stage);
        GameManager.MonsterManager.OnSpawned += (monster) => monster.OnDied += GameManager.StageManager.MoveNextStage;
        GameManager.MonsterManager.OnSpawned += (monster) => GameManager.StageManager.AddStage(monster.GetComponent<Stage>());
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(GameManager.ResourceManager.GetCache<GameObject>("PlayerCharacter.prefab")).GetComponent<PlayerCharacter>();
        player.ChangeCharacter(GameManager.DataManager.GetData<DB_PlayerCharacter>("Archer"));
        player.transform.position = GameManager.DataManager.GetData<DB_WorldSettings>("WorldSettings").PlayerSpawnPosition;
    }

    private void CreateUI()
    {
        Destroy(_uiLoading.gameObject);
        GameManager.UIManager.ShowSceneUI<UI_MainScene>();
    }

    private void StartGame()
    {
        InitializeStage();
        SpawnPlayer();
        CreateUI();
        GameManager.StageManager.MoveNextStage();
    }

    public override void UnloadScene()
    {
        GameManager.StageManager.Clear();
        GameManager.MonsterManager.Clear();
    }

    public void OnApplicationQuit()
    {
        GameManager.FirebaseDBManager.Write();
    }
}