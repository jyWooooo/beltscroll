using UnityEngine;

public class MonsterManager
{
    private Vector3 _spawnPosition;
    private int _spawnCount;
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

    public GameObject SpawnMonster()
    {
        var monsterName = GetNextMonsterName();
        monsterName = monsterName.Replace(" ", "");
        var prefab = GameManager.ResourceManager.GetCache<GameObject>($"Monster{monsterName}.prefab");
        CurrentMonster = Object.Instantiate(prefab, _spawnPosition, Quaternion.identity).GetComponent<MonsterBase>();
        CurrentMonster.OnDied += () => CurrentMonster = null;
        CurrentMonster.OnDied += () => GameManager.StageManager.MoveNextStage();
        GameManager.StageManager.AddStage(CurrentMonster.GetComponent<Stage>());
        OnSpawned?.Invoke(CurrentMonster);
        _spawnCount++;
        return CurrentMonster.gameObject;
    }

    public string GetNextMonsterName()
    {
        return _monsterNames[_spawnCount % _monsterNames.Length];
    }
}