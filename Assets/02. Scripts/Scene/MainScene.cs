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

                GameManager.StageManager.CreateStage();
                GameManager.StageManager.OnNextStageMoveFinished += () => GameManager.MonsterManager.SpawnMonster();
                var player = Instantiate(GameManager.ResourceManager.GetCache<GameObject>("PlayerCharacter.prefab")).GetComponent<PlayerCharacter>();
                player.ChangeCharacter(GameManager.DataManager.GetData<DB_PlayerCharacter>("Archer"));
                player.transform.position = GameManager.DataManager.GetData<DB_WorldSettings>("WorldSettings").PlayerSpawnPosition;
                GameManager.MonsterManager.SpawnMonster();
                GameManager.UIManager.ShowSceneUI<UI_MonsterInfo>();
            }
        });
    }

    public override void UnloadScene()
    {
    }
}