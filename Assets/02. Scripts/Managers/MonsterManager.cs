using UnityEngine;

public class MonsterManager
{
    private Vector3 _spawnPosition;
    private string[] _monsterNames;

    public DB_MonsterData MonsterData { get; private set; }
    public MonsterBase CurrentMonster { get; private set; }

    public event System.Action<MonsterBase> OnSpawned;

    public void Initialize()
    {
        _spawnPosition = GameManager.DataManager.GetData<DB_WorldSettings>("WorldSettings").MonsterSpawnPosition;
        MonsterData = GameManager.DataManager.GetData<DB_MonsterData>("MonsterData");
        _monsterNames = MonsterData.GetMonsterNames();
    }

    public void Clear()
    {
        foreach (var d in OnSpawned.GetInvocationList())
            OnSpawned -= (System.Action<MonsterBase>)d;
    }

    public GameObject SpawnMonster(int stageCnt)
    {
        var monsterName = GetNextMonsterName(stageCnt);
        monsterName = monsterName.Replace(" ", "");
        var prefab = GameManager.ResourceManager.GetCache<GameObject>($"Monster{monsterName}.prefab");
        CurrentMonster = Object.Instantiate(prefab, _spawnPosition, Quaternion.identity).GetComponent<MonsterBase>();
        CurrentMonster.OnDied += () => CurrentMonster = null;
        OnSpawned?.Invoke(CurrentMonster);
        return CurrentMonster.gameObject;
    }

    public string GetNextMonsterName(int stageCnt)
    {
        return _monsterNames[stageCnt % _monsterNames.Length];
    }
}