using UnityEngine;

public class MainScene : SceneBase
{
    public override void Start()
    {
        base.Start();
    }

    public override void LoadScene()
    {
        GameManager.ResourceManager.LoadAllAsync<Object>("MainScene", (key, cnt, total) => 
        {
            if (cnt == total)
            {
                Debug.Log("MainScene Resource Load Complete");

                InitializeStage();
                SpawnPlayer();
                CreateUI();
                GameManager.StageManager.MoveNextStage();
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
        GameManager.UIManager.ShowSceneUI<UI_MonsterInfo>();
        GameManager.UIManager.ShowSceneUI<UI_StageInfo>();
    }

    public override void UnloadScene()
    {
        GameManager.StageManager.Clear();
        GameManager.MonsterManager.Clear();
    }
}