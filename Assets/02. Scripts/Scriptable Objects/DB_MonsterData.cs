using System.Collections.Generic;
using UnityEngine;

public class DB_MonsterData : ScriptableObject
{
    private Dictionary<string, MonsterStatus> _dict;

    [field: SerializeField] public MonsterStatus[] MonsterStatuses { get; private set; }

    public void Set(MonsterStatus[] data)
    {
        MonsterStatuses = data;
    }

    public MonsterStatus Get(string name)
    {
        if (_dict == null) 
            Awake();
        return _dict[name];
    }

    private void Awake()
    {
        _dict = new();

        if (MonsterStatuses == null)
            return;

        foreach (var status in MonsterStatuses)
            _dict.Add(status.Name, status);
    }
}