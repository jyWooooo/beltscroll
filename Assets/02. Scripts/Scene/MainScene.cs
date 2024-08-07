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

                Instantiate(GameManager.ResourceManager.GetCache<GameObject>("Map.prefab"));
                var player = Instantiate(GameManager.ResourceManager.GetCache<GameObject>("PlayerCharacter.prefab")).GetComponent<PlayerCharacter>();
                player.ChangeCharacter(GameManager.DataManager.GetData<DB_PlayerCharacter>("Archer"));
                player.transform.position = new(-5f, -4.2f, 0f);
            }
        });
    }

    public override void UnloadScene()
    {
    }
}