using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private Dictionary<string, ScriptableObject> _db = new();
    private UserGUIDManager _userGUIDManager = new();

    public bool IsDone { get; private set; } = false;
    public Guid UserID => _userGUIDManager.GetUserID();

    public void Load(Action completed = null)
    {
        GameManager.ResourceManager.LoadAllAsync<UnityEngine.Object>("DB", (key, cnt, total) =>
        {
            CallbackResourceDBLoad(key, cnt, total, completed);
        });
    }

    public T GetData<T>(string key) where T : ScriptableObject
    {
        if (!_db.TryGetValue($"DB_{key}.data", out var data))
            return null;
        return data as T;
    }

    private void CallbackResourceDBLoad(string key, int cnt, int total, Action completed) 
    {
        _db.Add(key, GameManager.ResourceManager.GetCache<ScriptableObject>(key));

        if (cnt == total)
        {
            Debug.Log("DataManager DBLoad Complete");
            completed?.Invoke();
            IsDone = true;
        }
    }
}