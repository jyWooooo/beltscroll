using System;
using System.Collections;
using UnityEngine;

public class MainScene : SceneBase
{
    [SerializeField] private UI_Loading _uiLoading;
    private bool _isFirebaseDBLoaded = false;
    private bool _isMainSceneResourcesLoaded = false;

    public override void LoadScene()
    {
        ReadFirebaseDB(() => { _isFirebaseDBLoaded = true; });
        LoadResources(() => { _isMainSceneResourcesLoaded = true; });
        StartCoroutine(StartGame());
    }

    private void ReadFirebaseDB(Action callback)
    {
        _uiLoading.SetMessage("Checking Network Connection", 0);
        GameManager.FirebaseDBManager.TryRead((result) =>
        {
            Debug.Log($"Firebase Database Read Success. Value : {result}");
            GameManager.StageManager.SetStageCount(result - 1);
            _uiLoading.RemoveMessage("Checking Network Connection");
            callback?.Invoke();
        }, () =>
        {
            Debug.Log("Firebase Database Read Fail");
            _uiLoading.RemoveMessage("Checking Network Connection");
            callback?.Invoke();
        });
    }

    private void LoadResources(Action callback)
    {
        _uiLoading.SetMessage("Loading Resources", 1);
        GameManager.ResourceManager.LoadAllAsync<UnityEngine.Object>("MainScene", (key, cnt, total) =>
        {
            if (cnt == total)
            {
                Debug.Log("MainScene Resources Load Complete");
                _uiLoading.RemoveMessage("Loading Resources");
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

    private IEnumerator StartGame()
    {
        while (!(_isFirebaseDBLoaded && _isMainSceneResourcesLoaded))
            yield return null;

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