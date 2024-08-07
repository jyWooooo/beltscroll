using UnityEngine;

public class MonsterManager
{
    private Vector3 _spawnPosition;

    public MonsterBase CurrentMonster { get; private set; }

    public void Initialize()
    {
        _spawnPosition = GameManager.DataManager.GetData<DB_WorldSettings>("WorldSettings").MonsterSpawnPosition;
    }

    public GameObject SpawnMonster(string monsterName)
    {
        var prefab = GameManager.ResourceManager.GetCache<GameObject>($"Monster{monsterName}.prefab");
        CurrentMonster = Object.Instantiate(prefab, _spawnPosition, Quaternion.identity).GetComponent<MonsterBase>();
        return CurrentMonster.gameObject;
    }
}